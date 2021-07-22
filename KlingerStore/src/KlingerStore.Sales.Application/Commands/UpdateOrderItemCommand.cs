using FluentValidation;
using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }        
        public Guid ProductId { get; private set; }        
        public int Quantity { get; private set; }

        public UpdateOrderItemCommand(Guid clientId,Guid productId, int quantity)
        {
            AggregateId = productId;
             
            ClientId = clientId;            
            ProductId = productId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
                        
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade minimo de um item é 1");

            RuleFor(x => x.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade maxima de um item é 15");
        }
    }
}

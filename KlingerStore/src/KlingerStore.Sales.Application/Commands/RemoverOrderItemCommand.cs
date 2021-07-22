using FluentValidation;
using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class RemoverOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }        

        public Guid ProductId { get; private set; }

        public RemoverOrderItemCommand(Guid clientId, Guid productId)
        {
            ClientId = clientId;            
            ProductId = productId;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoverOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverOrderItemValidation : AbstractValidator<RemoverOrderItemCommand>
    {
        public RemoverOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }    
}

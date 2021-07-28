using FluentValidation;
using KlingerStore.Core.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public string Image { get; private set; }

        public AddOrderItemCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue, string image)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
            Image = image;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");
            
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Onome do produto não foi informado");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade minimo de um item é 1");

            RuleFor(x => x.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade maxima de um item é 15");

            RuleFor(x => x.UnitValue)
                .GreaterThan(0)
                .WithMessage("O Valor do item precisa ser maior que 0");
        }
    }
}

using FluentValidation;
using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public string NameCart { get; private set; }
        public string NumberCart { get; private set; }
        public string ExpiracaoCart { get; private set; }
        public string CvvCart { get; private set; }

        public StartOrderCommand(Guid orderId, Guid clientId, decimal total, string nameCart, string numberCart, string expiracaoCart, string cvvCart)
        {
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            NameCart = nameCart;
            NumberCart = numberCart;
            ExpiracaoCart = expiracaoCart;
            CvvCart = cvvCart;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.NameCart)
                .CreditCard()
                .WithMessage("O nome no cartão não foi informado");

            RuleFor(x => x.NumberCart)
                .NotEmpty()
                .WithMessage("O número do cartão não foi informado");

            RuleFor(x => x.ExpiracaoCart)
                .NotEmpty()
                .WithMessage("Data de expiração não foi informado");

            RuleFor(x => x.CvvCart)
                .NotEmpty()
                .WithMessage("O CVV não foi preenchido corretamente");
            
            RuleFor(x => x.CvvCart)
                .Length(3,4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}

using FluentValidation;
using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }        
        public string VoucherCode { get; private set; }

        public ApplyVoucherOrderItemCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;            
            VoucherCode = voucherCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderItemValidation : AbstractValidator<ApplyVoucherOrderItemCommand>
    {
        public ApplyVoucherOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");
                       
            RuleFor(x => x.VoucherCode)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}

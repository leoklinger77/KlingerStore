using FluentValidation;
using FluentValidation.Results;
using KlingerStore.Core.Domain.DomainObjects;
using KlingerStore.Sales.Domain.Class.Enumeration;
using System;
using System.Collections.Generic;

namespace KlingerStore.Sales.Domain.Class
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UsageDate { get; private set; }
        public DateTime ValidationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public ICollection<Order> Orders { get; set; }
               
        internal ValidationResult ValidateIfApplicable()
        {
            return new ValidateIfApplicableValidation().Validate(this);
        }
    }

    public class ValidateIfApplicableValidation : AbstractValidator<Voucher>
    {
        public ValidateIfApplicableValidation()
        {
            RuleFor(x => x.ValidationDate)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Este voucher está expirado.");

            RuleFor(x => x.Active)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(x => x.Used)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível.");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime validationDate)
        {
            return validationDate >= DateTime.Now;
        }
    }
}

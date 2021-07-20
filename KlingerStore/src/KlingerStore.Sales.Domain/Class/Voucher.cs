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
    }
}

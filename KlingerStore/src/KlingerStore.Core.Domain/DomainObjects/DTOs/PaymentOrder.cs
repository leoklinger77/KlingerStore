using System;

namespace KlingerStore.Core.Domain.DomainObjects.DTOs
{
    public class PaymentOrder
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Total { get; set; }

        public string NameCart { get;  set; }
        public string NumberCart { get;  set; }
        public string ExpiracaoCart { get;  set; }
        public string CvvCart { get;  set; }

    }
}

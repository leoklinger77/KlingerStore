using KlingerStore.Core.Domain.DomainObjects;
using KlingerStore.Core.Domain.Interfaces;
using System;

namespace KlingerStore.Payment.Domain.Class
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }

        public string NameCart { get; set; }
        public string NumberCart { get; set; }
        public string ExpiracaoCart { get; set; }
        public string CvvCart { get; set; }

        public Transaction Transaction { get; set; }
    }
}

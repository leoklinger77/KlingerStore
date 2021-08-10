using KlingerStore.Payment.Domain.Class.Enum;
using System;

namespace KlingerStore.Payment.Domain.Class
{
    public class Transaction
    {
        public Guid OrderId { get; set; }        
        public Guid PaymentId { get; set; }
        public decimal TotalValue { get; set; }
        public StatusTransaction StatusTransaction { get; set; }

        public Payment Payment { get; set; }
    }    
}

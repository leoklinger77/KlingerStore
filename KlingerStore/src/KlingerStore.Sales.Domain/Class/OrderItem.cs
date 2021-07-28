using KlingerStore.Core.Domain.DomainObjects;
using System;

namespace KlingerStore.Sales.Domain.Class
{
    public class OrderItem : Entity
    {
        public Order Order { get; private set; }
        public Guid OrderId { get; private set; }
        
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public string Image { get; private set; }

        protected OrderItem() { }

        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue, string image)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
            Image = image;
        }

        public void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }
        public decimal CalculateValue()
        {
            return Quantity * UnitValue;
        }
        public void AddUnity(int unity)
        {
            Quantity += unity;
        }
        public void UpdateUnity(int unity)
        {
            Quantity = unity;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}

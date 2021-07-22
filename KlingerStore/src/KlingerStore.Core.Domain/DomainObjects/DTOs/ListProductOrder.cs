using System;
using System.Collections.Generic;

namespace KlingerStore.Core.Domain.DomainObjects.DTOs
{
    public class ListProductOrder
    {
        public Guid OrderId { get; private set; }
        public ICollection<Item> Items { get; private set; }

        public ListProductOrder(Guid orderId, ICollection<Item> items)
        {
            OrderId = orderId;
            Items = items;
        }
    }
    public class Item
    {
        public Guid Id { get; private set; }
        public int Quantity { get; private set; }

        public Item(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}

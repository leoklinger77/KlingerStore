using KlingerStore.Core.Domain.DomainObjects;
using System;

namespace KlingerStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime InsertDate { get; private set; }
        public string Image { get; private set; }
        public int QuantityStock { get; private set; }

        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime insertDate, string image)
        {
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            InsertDate = insertDate;
            Image = image;
            CategoryId = categoryId;
        }

        public void Activate() => Active = true;
        public void Disable() => Active = false;
        public void ChangeCategory(Category category)
        {
            CategoryId = category.Id;
            Category = category;
        }
        public void ChageDescription(string description)
        {
            Description = description;
        }
        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            QuantityStock -= quantity;
        }
        public void ReplenishStock(int quantity)
        {            
            QuantityStock += quantity;
        }
        public bool HasStock(int quantity)
        {
            return QuantityStock >= quantity;
        }

        public void Validite()
        {

        }


    }
}

using KlingerStore.Core.Domain.DomainObjects;
using KlingerStore.Core.Domain.Interfaces;
using System;

namespace KlingerStore.Catalog.Domain.Class
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime InsertDate { get; private set; }
        public string Image { get; private set; }
        public int QuantityStock { get; private set; }
        protected Product() { }

        public Product(Guid categoryId, string name, string description, bool active, decimal value, DateTime insertDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            InsertDate = insertDate;
            Image = image;
            Dimensions = dimensions;

            Validite();            
        }
        public void Activate() => Active = true;
        public void Disable() => Active = false;
        public void ChangeCategory(Category category)
        {            
            CategoryId = category.Id;
            Validation.ValidateIfdifferent(CategoryId, Guid.Empty, "O Campo CategoryId do produto não pode estar vazio");
            Category = category;
        }
        public void ChageDescription(string description)
        {
            Validation.ValidateIsNullOrEmpty(Description, "O Campo Nome do produto não pode estar vazio");
            Description = description;
        }
        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasStock(quantity)) throw new DomainException("Estoque insuficiente");
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
            Validation.ValidateIsNullOrEmpty(Name, "O campo Nome do produto não pode estar vazio");
            Validation.ValidateIsNullOrEmpty(Description, "O campo Descricao do produto não pode estar vazio");
            Validation.ValidateIfLessThan(Value, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validation.ValidateIfEqual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");            
            Validation.ValidateIsNullOrEmpty(Image, "O campo Imagem do produto não pode estar vazio");
        }
    }
}

using System;

namespace KlingerStore.Sales.Application.Querys.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ValorUnit { get; set; }
        public decimal TotalValue { get; set; }
    }
}

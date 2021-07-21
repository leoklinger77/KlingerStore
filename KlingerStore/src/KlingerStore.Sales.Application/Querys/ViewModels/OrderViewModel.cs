using System;

namespace KlingerStore.Sales.Application.Querys.ViewModels
{
    public class OrderViewModel
    {
        public int Code { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime InsertDate { get; set; }
        public int OrderStatus { get; set; }
    }
}

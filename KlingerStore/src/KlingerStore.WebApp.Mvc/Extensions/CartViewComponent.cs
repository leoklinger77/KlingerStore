using KlingerStore.Sales.Application.Querys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQuerys _orderQuerys;

        protected Guid ClientId = Guid.Parse("247fbda9-d54a-463e-bcf0-c2e93f33c606");

        public CartViewComponent(IOrderQuerys orderQuerys)
        {
            _orderQuerys = orderQuerys;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQuerys.FindCartClient(ClientId);
            var items = cart?.Items.Count ?? 0;

            return View(items);
        }        
    }
}

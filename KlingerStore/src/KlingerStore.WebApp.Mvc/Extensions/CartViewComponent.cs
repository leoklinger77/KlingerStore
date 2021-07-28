using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Sales.Application.Querys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQuerys _orderQuerys;
        private readonly IUser _user;

        protected async Task<Guid> ClientId() => await _user.ClientId();        

        public CartViewComponent(IOrderQuerys orderQuerys, IUser user)
        {
            _orderQuerys = orderQuerys;
            _user = user;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQuerys.FindCartClient(await ClientId());
            var items = cart?.Items.Count ?? 0;

            return View(items);
        }        
    }
}

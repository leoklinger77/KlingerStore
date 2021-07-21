using KlingerStore.Sales.Application.Querys.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Querys
{
    public interface IOrderQuerys
    {
        Task<CartViewModel> FindCartClient(Guid clientId);
        Task<IEnumerable<OrderViewModel>> FindOrderClient(Guid clientId);
    }
}

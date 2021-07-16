using KlingerStore.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> FindByCateory(int code);
        Task<ProductViewModel> FindById(Guid id);
        Task<IEnumerable<ProductViewModel>> FindAllsProduct();
        Task<IEnumerable<CategoryViewModel>> FIndAllsCategory();

        Task InsertProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DebitStock(Guid id, int quantity);
        Task<ProductViewModel> ReplenishStock(Guid id, int quantity);
    }
}

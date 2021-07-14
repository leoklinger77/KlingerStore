using KlingerStore.Catalog.Domain.Class;
using KlingerStore.Core.Domain.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindById(Guid id);
        Task<IEnumerable<Product>> FindByCategory(int code);
        Task<IEnumerable<Category>> FindCategory();

        Task Insert(Product product);
        Task Update(Product product);

        Task Insert(Category category);
        Task Update(Category category);
    }
}

using KlingerStore.Catalog.Domain.Class;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Core.Domain.Data.Interfaces;
using KlingeStore.Catalog.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlingeStore.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context?.DisposeAsync();
        }

        public async Task<IEnumerable<Product>> FindAll()
        {
            return await _context.Product.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> FindByCategory(int code)
        {
            return await _context.Product.Where(x=>x.Category.Code == code).AsNoTracking().ToListAsync();
        }

        public async Task<Product> FindById(Guid id)
        {
            return await _context.Product.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> FindCategory()
        {
            return await _context.Category.AsNoTracking().ToListAsync();
        }

        public async Task Insert(Product product)
        {
            await _context.Product.AddAsync(product);            
        }

        public async Task Insert(Category category)
        {
            await _context.Category.AddAsync(category);            
        }

        public async Task Update(Product product)
        {
            _context.Product.Update(product);            
        }

        public async Task Update(Category category)
        {
            _context.Category.Update(category);            
        }
    }
}

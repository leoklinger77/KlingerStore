using AutoMapper;
using KlingerStore.Catalog.Application.ViewModels;
using KlingerStore.Catalog.Domain.Class;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Core.Domain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IMapper mapper, IProductRepository productRepository, IStockService stockService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _stockService = stockService;
        }

        public async Task<ProductViewModel> FindById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id));
        }
        public async Task<IEnumerable<CategoryViewModel>> FIndAllsCategory()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.FindCategory());
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllsProduct()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.FindAll());
        }

        public async Task<IEnumerable<ProductViewModel>> FindByCateory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.FindByCategory(code));
        }

        public async Task<ProductViewModel> DebitStock(Guid id, int quantity)
        {
            if(!_stockService.DebitStock(id, quantity).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id));
        }

        public async Task<ProductViewModel> ReplenishStock(Guid id, int quantity)
        {
            if (!_stockService.ReplenishStock(id, quantity).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id));
        }

        public async Task InsertProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            await _productRepository.Insert(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            await _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }
    }
}

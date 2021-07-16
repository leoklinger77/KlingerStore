using KlingerStore.Catalog.Application.Services;
using KlingerStore.Catalog.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers.Administration
{
    public class AdminProductController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("adm-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.FindAllsProduct());
        }
        [HttpGet]
        [Route("novo-produto")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopularCategories(new ProductViewModel()));
        }
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(await PopularCategories(new ProductViewModel()));

            await _productAppService.InsertProduct(viewModel);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View(await PopularCategories(await _productAppService.FindById(id)));
        }
        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel viewModel)
        {
            var produt = await _productAppService.FindById(id);
            viewModel.QuantityStock = produt.QuantityStock;

            ModelState.Remove("QuantityStock");
            if(!ModelState.IsValid) return View(await PopularCategories(await _productAppService.FindById(id)));

            await _productAppService.UpdateProduct(viewModel);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.FindById(id));
        }        
        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.ReplenishStock(id, quantity);
            }
            else
            {
                await _productAppService.DebitStock(id, quantity);
            }

            return View(nameof(Index), await _productAppService.FindAllsProduct());
        }

        private async Task<ProductViewModel> PopularCategories(ProductViewModel product)
        {
            product.Categorys = await _productAppService.FIndAllsCategory();
            return product;
        }
    }
}

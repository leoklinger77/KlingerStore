using KlingerStore.Catalog.Application.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Sales.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatrHandler _mediatrHandler;

        public CartController(IMediatrHandler mediatrHandler, INotificationHandler<DomainNotification> notification,
                            IProductAppService productAppService)
                            : base(notification, mediatrHandler)
        {
            _productAppService = productAppService;
            _mediatrHandler = mediatrHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.FindById(id);
            if (product is null) return BadRequest();

            if (product.QuantityStock < quantity)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProductDetails", "Vitrini", new { id });
            }

            var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, quantity, product.Value);

            await _mediatrHandler.SendCommand(command);
            
            if (OperationValidit())
            {
                return RedirectToAction("ProductDetails", "Vitrini", new { id });
            }

            TempData["Erros"] = FindMessageError();
            return RedirectToAction("ProductDetails", "Vitrini", new { id });
        }

    }
}

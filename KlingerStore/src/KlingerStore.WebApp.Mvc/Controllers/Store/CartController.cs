using KlingerStore.Catalog.Application.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Sales.Application.Commands;
using KlingerStore.Sales.Application.Querys;
using KlingerStore.Sales.Application.Querys.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatrHandler _mediatrHandler;
        private readonly IOrderQuerys _orderQuerys;

        public CartController(IMediatrHandler mediatrHandler, INotificationHandler<DomainNotification> notification,
                            IProductAppService productAppService, IOrderQuerys orderQuerys)
                            : base(notification, mediatrHandler)
        {
            _productAppService = productAppService;
            _mediatrHandler = mediatrHandler;
            _orderQuerys = orderQuerys;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderQuerys.FindCartClient(ClientId));
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
                return RedirectToAction("Index");
            }

            TempData["Erros"] = FindMessageError();
            return RedirectToAction("ProductDetails", "Vitrini", new { id });
        }
        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var product = await _productAppService.FindById(id);
            if (product is null) return BadRequest();

            var command = new RemoverOrderItemCommand(ClientId, id);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(ClientId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var order = await _productAppService.FindById(id);
            if (order is null) return BadRequest();

            var command = new UpdateOrderItemCommand(ClientId, id, quantity);
            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(ClientId));
        }
        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderItemCommand(ClientId, voucherCode);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(ClientId));

        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> PurchaseSummary()
        {
            return View(await _orderQuerys.FindCartClient(ClientId));
        }

        [HttpPost]
        [Route("inciar-pedido")]
        public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
        {
            var cart = await _orderQuerys.FindCartClient(ClientId);

            var command = new StartOrderCommand(cart.OrderId, ClientId, cart.TotalValue, cart.Payment.CardName, 
                                                cart.Payment.NumberCart, cart.Payment.ExpirationCart, cart.Payment.CvvCart);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index", "Order");
            }

            return View("PurchaseSummary", await _orderQuerys.FindCartClient(ClientId));
        }

    }
}

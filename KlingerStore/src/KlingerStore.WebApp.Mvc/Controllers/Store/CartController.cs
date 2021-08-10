using KlingerStore.Catalog.Application.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Sales.Application.Commands;
using KlingerStore.Sales.Application.Querys;
using KlingerStore.Sales.Application.Querys.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatrHandler _mediatrHandler;
        private readonly IOrderQuerys _orderQuerys;        

        public CartController(IMediatrHandler mediatrHandler, INotificationHandler<DomainNotification> notification,
                            IProductAppService productAppService, IOrderQuerys orderQuerys, IUser user)
                            : base(notification, mediatrHandler, user)
        {
            _productAppService = productAppService;
            _mediatrHandler = mediatrHandler;
            _orderQuerys = orderQuerys;            
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderQuerys.FindCartClient(await _user.ClientId()));
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

            var command = new AddOrderItemCommand(await _user.ClientId(), product.Id, product.Name, quantity, product.Value, product.Image);

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

            var command = new RemoverOrderItemCommand(await _user.ClientId(), id);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(await _user.ClientId()));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var order = await _productAppService.FindById(id);
            if (order is null) return BadRequest();

            var command = new UpdateOrderItemCommand(await _user.ClientId(), id, quantity);
            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(await _user.ClientId()));
        }
        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderItemCommand(await _user.ClientId(), voucherCode);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuerys.FindCartClient(await _user.ClientId()));

        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> PurchaseSummary()
        {
            return View(await _orderQuerys.FindCartClient(await _user.ClientId()));
        }

        [HttpPost]
        [Route("inciar-pedido")]
        public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
        {
            if(!ModelState.IsValid) return View("PurchaseSummary", await _orderQuerys.FindCartClient(await _user.ClientId()));
            var cart = await _orderQuerys.FindCartClient(await _user.ClientId());

            var command = new StartOrderCommand(cart.OrderId, await _user.ClientId(), cart.TotalValue, cartViewModel.Payment.CardName,
                                                cartViewModel.Payment.NumberCart, cartViewModel.Payment.ExpirationCart, cartViewModel.Payment.CvvCart);

            await _mediatrHandler.SendCommand(command);

            if (OperationValidit())
            {
                return RedirectToAction("Index");
            }

            return View("PurchaseSummary", await _orderQuerys.FindCartClient(await _user.ClientId()));
        }

    }
}

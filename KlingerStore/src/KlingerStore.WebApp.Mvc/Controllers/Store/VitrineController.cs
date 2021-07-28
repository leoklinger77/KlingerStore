using KlingerStore.Catalog.Application.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public class VitrineController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public VitrineController(IMediatrHandler mediatrHandler, INotificationHandler<DomainNotification> notification, IProductAppService productAppService, IUser user)
                        : base(notification, mediatrHandler, user)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.FindAllsProduct());
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View(await _productAppService.FindById(id));
        }
    }
}

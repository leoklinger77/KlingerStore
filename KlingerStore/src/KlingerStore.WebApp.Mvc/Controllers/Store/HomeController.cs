using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.WebApp.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediatrHandler mediatrHandler, INotificationHandler<DomainNotification> notification,
                    ILogger<HomeController> logger) : base(notification, mediatrHandler)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

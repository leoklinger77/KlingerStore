using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notification;

        public SummaryViewComponent(INotificationHandler<DomainNotification> notification)
        {
            _notification = (DomainNotificationHandler)notification;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notification = await Task.FromResult(_notification.FindNotification());
            notification.ForEach(x => ViewBag.ModelState.AddModelError(string.Empty, x.Value));

            return View();
        }
    }
}

using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly IUser _user;        
        protected Guid ClientId = Guid.Parse("247fbda9-d54a-463e-bcf0-c2e93f33c606");

        private readonly DomainNotificationHandler _notification;
        private readonly IMediatrHandler _mediatrHandler;

        protected ControllerBase(INotificationHandler<DomainNotification> notification, IMediatrHandler mediatrHandler, IUser user)
        {
            _notification = (DomainNotificationHandler)notification;
            _mediatrHandler = mediatrHandler;
            _user = user;
        }

        protected bool OperationValidit()
        {
            return !_notification.HasNotification();
        }

        protected IEnumerable<string> FindMessageError()
        {
            return _notification.FindNotification().Select(x => x.Value).ToList();
        }

        protected void ErrorNotifier(string code, string message)
        {
            _mediatrHandler.PublishNotification(new DomainNotification(code, message));
        }
    }
}

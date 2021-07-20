using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid ClientId = Guid.Parse("247fbda9-d54a-463e-bcf0-c2e93f33c602");
    }
}

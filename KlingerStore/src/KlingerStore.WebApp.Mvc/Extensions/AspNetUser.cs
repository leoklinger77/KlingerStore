using KlingerStore.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KlingerStore.WebApp.Mvc.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<IdentityUser> _userManager;        
        public AspNetUser(IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;
        public async Task<Guid> ClientId() 
        {
            if (!IsAuthenticated()) return Guid.Empty;

            var identity = new IdentityUser { Email = Name };
            var logado = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            return Guid.Parse(logado.Id);

        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}

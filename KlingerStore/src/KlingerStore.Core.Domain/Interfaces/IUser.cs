using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Task<Guid> ClientId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}

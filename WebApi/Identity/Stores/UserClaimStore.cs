using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore
    {
        public Task<IList<Claim>> GetClaimsAsync(UserProfile user)
        {
            var claims = user.Claims.Select(e => new Claim(e.Type, e.Value));
            return Task.FromResult(claims.ToList() as IList<Claim>);
        }

        public Task AddClaimAsync(UserProfile user, Claim claim)
        {
            user.Claims.Add(new UserClaim
            {
                Type = claim.Type,
                Value = claim.Value
            });
            return Task.CompletedTask;
        }

        public Task RemoveClaimAsync(UserProfile user, Claim claim)
        {
            var userClaim = user.Claims.FirstOrDefault(e => e.Type == claim.Type && e.Value == claim.Value);
            if(userClaim == null) return Task.CompletedTask;
            user.Claims.Remove(userClaim);
            return Task.CompletedTask;
        }
    }
}

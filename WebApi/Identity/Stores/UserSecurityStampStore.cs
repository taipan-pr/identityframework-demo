using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore
    {
        public Task SetSecurityStampAsync(UserProfile user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(UserProfile user)
        {
            return Task.FromResult(user.SecurityStamp);
        }
    }
}

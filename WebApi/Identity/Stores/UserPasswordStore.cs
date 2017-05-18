using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore
    {
        public Task SetPasswordHashAsync(UserProfile user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(UserProfile user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(UserProfile user)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));
        }
    }
}

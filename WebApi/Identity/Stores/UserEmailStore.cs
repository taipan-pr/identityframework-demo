using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore
    {
        public Task SetEmailAsync(UserProfile user, string email)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(UserProfile user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserProfile user)
        {
            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(UserProfile user, bool confirmed)
        {
            user.IsEmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<UserProfile> FindByEmailAsync(string email)
        {
            return Task.FromResult(this.users.FirstOrDefault(e => e.Email == email));
        }
    }
}

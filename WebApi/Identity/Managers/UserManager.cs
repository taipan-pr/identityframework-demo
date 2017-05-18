using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Identity.Managers
{
    internal class UserManager : UserManager<UserProfile, Guid>, IUserManager
    {
        public UserManager(IUserStore<UserProfile, Guid> store) : base(store)
        {
        }

        public override async Task<IdentityResult> SetEmailAsync(Guid userId, string email)
        {
            var result = await base.SetEmailAsync(userId, email);
            if (!result.Succeeded) return result;

            var token = await this.GenerateEmailConfirmationTokenAsync(userId);
            await this.SendEmailAsync(userId, "Confirm Email Token", $"?id={userId}&token={HttpUtility.UrlEncode(token)}");

            return result;
        }
    }
}

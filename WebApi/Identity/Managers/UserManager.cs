using System;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Identity.Managers
{
    internal class UserManager : UserManager<UserProfile, Guid>, IUserManager
    {
        public UserManager(IUserStore<UserProfile, Guid> store) : base(store)
        {
        }
    }
}

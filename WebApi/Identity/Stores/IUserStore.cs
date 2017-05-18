using System;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal interface IUserStore : IUserStore<UserProfile, Guid>
    {
    }
}

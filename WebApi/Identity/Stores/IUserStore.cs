using System;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal interface IUserStore : IUserStore<UserProfile, Guid>,
                                    IQueryableUserStore<UserProfile, Guid>,
                                    IUserPasswordStore<UserProfile, Guid>,
                                    IUserSecurityStampStore<UserProfile, Guid>,
                                    IUserEmailStore<UserProfile, Guid>,
                                    IUserClaimStore<UserProfile, Guid>
    {
    }
}

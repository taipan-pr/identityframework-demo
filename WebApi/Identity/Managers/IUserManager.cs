using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Identity.Managers
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(UserProfile user);
        Task<UserProfile> FindByIdAsync(Guid userId);
        Task<UserProfile> FindByNameAsync(string userName);
        Task<IdentityResult> DeleteAsync(UserProfile user);
        Task<IdentityResult> UpdateAsync(UserProfile user);

        IQueryable<UserProfile> Users { get; }

        Task<IdentityResult> CreateAsync(UserProfile user, string password);

        Task<UserProfile> FindByEmailAsync(string email);
        Task<IdentityResult> SetEmailAsync(Guid userId, string email);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
    }
}

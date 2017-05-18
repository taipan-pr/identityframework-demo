using System;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal class UserStore : IUserStore
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> FindByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Linq;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore
    {
        public IQueryable<UserProfile> Users => this.users.AsQueryable();
    }
}

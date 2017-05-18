using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Identity.Stores
{
    internal partial class UserStore : IUserStore
    {
        private readonly string filePath;
        private IList<UserProfile> users;

        public UserStore()
        {
            this.filePath = $"{HttpRuntime.AppDomainAppPath}bin\\users.txt";
            this.LoadUserData();
        }

        private void LoadUserData()
        {
            if (!File.Exists(this.filePath))
            {
                this.users = new List<UserProfile>();
                return;
            }
            var content = File.ReadAllText(this.filePath);
            this.users = string.IsNullOrWhiteSpace(content)
                ? new List<UserProfile>()
                : JsonConvert.DeserializeObject<IList<UserProfile>>(content);
        }

        private void SaveUserData()
        {
            var content = JsonConvert.SerializeObject(this.users, Formatting.Indented);
            File.WriteAllText(this.filePath, content);
        }

        public void Dispose()
        {
            this.users = null;
        }

        public Task CreateAsync(UserProfile user)
        {
            this.users.Add(user);
            this.SaveUserData();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(UserProfile user)
        {
            this.SaveUserData();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(UserProfile user)
        {
            this.users.Remove(user);
            this.SaveUserData();
            return Task.CompletedTask;
        }

        public Task<UserProfile> FindByIdAsync(Guid userId)
        {
            return Task.FromResult(this.users.FirstOrDefault(e => e.Id == userId));
        }

        public Task<UserProfile> FindByNameAsync(string userName)
        {
            return Task.FromResult(this.users.FirstOrDefault(e => e.UserName == userName));
        }
    }
}

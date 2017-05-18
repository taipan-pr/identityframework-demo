using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Identity.Managers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("User")]
    public class UserProfileController : ApiController
    {
        private readonly IUserManager manager;

        public UserProfileController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        [Route]
        public HttpResponseMessage GetAsync()
        {
            var result = this.manager.Users.ToList();
            return result.Any()
                ? this.Request.CreateResponse(HttpStatusCode.OK, result)
                : this.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> GetByIdAsync([FromUri] Guid id)
        {
            var result = await this.manager.FindByIdAsync(id);
            return result == null
                ? this.Request.CreateResponse(HttpStatusCode.NotFound)
                : this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> GetByNameAsync([FromUri] string username)
        {
            var result = await this.manager.FindByNameAsync(username);
            return result == null
                ? this.Request.CreateResponse(HttpStatusCode.NotFound)
                : this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route]
        public async Task<HttpResponseMessage> CreateAsync(CreateUserProfile request)
        {
            var result = string.IsNullOrWhiteSpace(request.Password)
                ? await this.manager.CreateAsync(new UserProfile
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                })
                : await this.manager.CreateAsync(new UserProfile
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                }, request.Password);
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            var user = await this.manager.FindByIdAsync(id);
            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var result = await this.manager.DeleteAsync(user);
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<HttpResponseMessage> UpdateAsync([FromUri] Guid id, UpdateUserProfile request)
        {
            var user = await this.manager.FindByIdAsync(id);
            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            user.UserName = request.UserName;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            var result = await this.manager.UpdateAsync(user);
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }
    }
}

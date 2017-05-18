using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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

        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> GetByEmailAsync([FromUri] string email)
        {
            var result = await this.manager.FindByEmailAsync(email);
            return result == null
                ? this.Request.CreateResponse(HttpStatusCode.NotFound)
                : this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> VerifyEmailAsync([FromUri] Guid id, string token)
        {
            var result = await this.manager.ConfirmEmailAsync(id, token);
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        [HttpPost]
        [Route]
        public async Task<HttpResponseMessage> CreateAsync(CreateUserProfile request)
        {
            var id = Guid.NewGuid();
            var result = string.IsNullOrWhiteSpace(request.Password)
                ? await this.manager.CreateAsync(new UserProfile
                {
                    Id = id,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                })
                : await this.manager.CreateAsync(new UserProfile
                {
                    Id = id,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                }, request.Password);
            if (!result.Succeeded)
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);

            result = await this.manager.SetEmailAsync(id, request.Email);
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
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateUserProfile request)
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
            if (user.Email != request.Email)
            {
                if (!result.Succeeded)
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                result = await this.manager.SetEmailAsync(user.Id, request.Email);
            }
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        [HttpPost]
        [Route("{id}/Claims")]
        public async Task<HttpResponseMessage> AddClaimAsync(Guid id, UserClaim claim)
        {
            var user = await this.manager.FindByIdAsync(id);
            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var result = await this.manager.AddClaimAsync(id, new Claim(claim.Type, claim.Value));
            return result.Succeeded
                ? this.Request.CreateResponse(HttpStatusCode.OK)
                : this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        [HttpGet]
        [Route("{id}/Claims")]
        public async Task<HttpResponseMessage> GetClaimsAsync(Guid id)
        {
            var user = await this.manager.FindByIdAsync(id);
            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var result = await this.manager.GetClaimsAsync(id);
            return result.Any()
                ? this.Request.CreateResponse(HttpStatusCode.OK, result.Select(e => new UserClaim
                {
                    Type = e.Type,
                    Value = e.Value
                }))
                : this.Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("User")]
    public class UserProfileController : ApiController
    {
        [HttpGet]
        [Route]
        public HttpResponseMessage TestResponseMessage()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

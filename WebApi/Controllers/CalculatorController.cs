using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("Calculator")]
    public class CalculatorController : ApiController
    {
        private readonly ICalculatorService _service;

        public CalculatorController(ICalculatorService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> Get(int a, int b)
        {
            if (a > 10 || b < 5)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = await this._service.AddNumberAsync(a, b);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}

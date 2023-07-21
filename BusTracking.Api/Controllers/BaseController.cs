using BusTracking.Core.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        protected APIResponse GetResponse(object data)
        {
            var response = new APIResponse(true, data);
            return response;
        }
    }
}

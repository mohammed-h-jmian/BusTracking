using BusTracking.Core.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.Api.Areas.Driver.Controllers
{
    [ApiController]
    [Area("Driver")]
    [Route("api/[area]/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        protected APIResponse GetResponse(object data)
        {
            var response = new APIResponse(true , data);
            return response;
        }

    }
}

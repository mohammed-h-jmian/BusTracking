using BusTracking.Core.Responses;
using BusTracking.Infrastructure.Services.LineService;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.Api.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILineService _line;
        public HomeController(ILineService line)
        {
            _line = line;
        }

        /// <summary>
        /// ( No Authorize  ) Retrieves the active line.
        /// </summary>
        /// <returns>The active line information.</returns>
        [HttpGet]
        public async Task<ActionResult> GetActiveLine()
        {
            try
            {
                var lines = await _line.GetActiveLine();
                if (lines == null)
                {
                    return Ok(new Response(false, "No Line Active !!!"));
                }
                return Ok(GetResponse(lines));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

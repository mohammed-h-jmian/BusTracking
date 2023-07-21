using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels;
using BusTracking.Data;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusTracking.Api.Controllers
{
    public class LineController : BaseController
    {
        private readonly ILineService _line;
        public LineController(ILineService line)
        {
            _line = line;
        }
        
      
        /// <summary>
        /// ( No Authorize  ) Retrieves the all active line.
        /// </summary>
        /// <returns>The active line information.</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllActiveLine()
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

        /// <summary>
        /// ( No Authorize  ) Retrieves the line information based on the specified lineId.
        /// </summary>
        /// <param name="lineId">The ID of the line.</param>
        /// <returns>The line information.</returns>
        [HttpGet("{lineId}")]
        public async Task<ActionResult> Get(int lineId)
        {
            try
            {
                var line = await _line.GetAPI(lineId);
                if (line == null)
                {
                    return Ok(new Response(false, "No Line !!!"));
                }
                return Ok(GetResponse(line));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}


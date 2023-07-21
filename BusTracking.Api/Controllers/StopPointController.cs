using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.StopPointService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Drawing;

namespace BusTracking.Api.Controllers
{
    public class StopPointController : BaseController
    {
        private readonly IStopPointService _point;
        public StopPointController(IStopPointService point)
        {
            _point = point;
        }

        /// <summary>
        /// ( No Authorize  ) Retrieves all stop points.
        /// </summary>
        /// <returns>Returns a list of stop points.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var points = await _point.GetAllAPI();
                if (points == null)
                {
                    return Ok(new Response(false, "No Stop Point !!!"));
                }
                return Ok(GetResponse(points));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// ( No Authorize  ) Retrieves the stop point information based on the specified stopPointId.
        /// </summary>
        /// <param name="stopPointId">The ID of the stop point.</param>
        /// <returns>The stop point information.</returns>
        [HttpGet("{stopPointId}")]
        public async Task<ActionResult> Get(int stopPointId)
        {
            try
            {
                var point = await _point.GetAPI(stopPointId);
                if (point == null)
                {
                    return Ok(new Response(false, "No Stop Point !!!"));
                }
                return Ok(GetResponse(point));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}

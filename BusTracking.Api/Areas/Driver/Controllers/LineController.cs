using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Exceptions;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.LineService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusTracking.Api.Areas.Driver.Controllers
{
    public class LineController : BaseController
    {
        private readonly ILineService _line;
        public LineController(ILineService line)
        {
            _line = line;
        }


        /// <summary>
        /// Retrieve all lines associated with a specific company.
        /// </summary>
        /// <remarks>
        /// This endpoint allows retrieving all lines that are associated with the specified company.
        /// The company is identified by the provided companyId query parameter.
        /// </remarks>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A response containing the list of lines.</returns>
        [HttpGet("{companyId}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll(int companyId)
        {
            try
            {
                var lines = await _line.GetAllByCompanyId(companyId);
                if (lines == null)
                {
                    return Ok(new Response(false, "There are no lines active for this company"));
                }
                return Ok(GetResponse(lines));
            }
            catch (Exception)
            {
                // Other error occurred
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Set the BusId for a line by providing the LineId and BusId in the request body.
        /// </summary>
        /// <remarks>
        /// This endpoint allows setting the BusId for a specific line by providing the LineId and BusId in the request body.
        /// </remarks>
        /// <param name="dto">The DTO containing the LineId and BusId.</param>
        /// <returns>A response indicating the success or failure of setting the BusId.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetBusInLineById([FromBody] SetBusInLineIdDto dto)
        {
            try
            {
                var line = await _line.SetBusInLine(dto);
                return Ok(GetResponse(line));
            }
            catch (EntityNotFoundException)
            {
                return Ok(new Response(false, "Line not found"));
            }
            catch (LineIsAlreadyLiveException)
            {
                return Ok(new Response(false, "This line is already in use"));
            }
            catch (LineIsActiveExpiredException)
            {
                return Ok(new Response(false, "The line has expired (Not Active)"));
            }
            catch (BusIdAlreadyUsedException)
            {
                return Ok(new Response(false, "This bus uses another line"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Set the BusId for a line by providing the LineCode and BusId in the request body.
        /// </summary>
        /// <remarks>
        /// This endpoint allows setting the BusId for a specific line identified by its LineCode.
        /// </remarks>
        /// <param name="dto">The DTO containing the LineCode and BusId.</param>
        /// <returns>A response indicating the success or failure of setting the BusId.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetBusInLineByCode([FromBody] SetBusInLineCodeDto dto)
        {
            try
            {
                var line = await _line.SetBusInLine(dto);
                return Ok(GetResponse(line));
            }
            catch (EntityNotFoundException)
            {
                return Ok(new Response(false, "Line not found"));
            }
            catch (LineIsAlreadyLiveException)
            {
                return Ok(new Response(false, "This line is already in use"));
            }
            catch (LineIsActiveExpiredException)
            {
                return Ok(new Response(false, "The line has expired (Not Active)"));
            }
            catch (BusIdAlreadyUsedException)
            {
                return Ok(new Response(false, "This bus uses another line"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


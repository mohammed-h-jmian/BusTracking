using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Exceptions;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.LineService;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusTracking.Api.Areas.Driver.Controllers
{
    public class BusController : BaseController
    {
        private readonly IBusService _bus;
        public BusController(IBusService bus)
        {
            _bus = bus;
        }
        /// <summary>
        /// Bus login endpoint.
        /// </summary>
        /// <remarks>
        /// This endpoint is used for bus login. It verifies the provided bus number and password,
        /// and if they are correct, it returns an access token and bus data.
        /// </remarks>
        /// <param name="dto">The login information for the bus.</param>
        /// <returns>An object containing the access token and bus data.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginBusDto dto)
        {

            try
            {
                var loginResult = await _bus.Login(dto);

                // Successful login
                return Ok(GetResponse(loginResult));
            }
            catch (InvalidUsernameOrPassword)
            {
                // Invalid username or password
                //return Unauthorized("Invalid Username or Password");
              
                return Ok( new Response(false , "Invalid Username or Password"));
            }
            catch (Exception)
            {
                // Other error occurred
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Share the location of a bus.
        /// </summary>
        /// <param name="busId">The ID of the bus.</param>
        /// <param name="dto">The location data.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("{busId}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShareBusLocation(int busId, [FromBody] LocationDto dto)
        {
            try
            {
                // Validate the input data, e.g., using ModelState.IsValid or custom validation logic

                var result = await _bus.ShareLocation(busId, dto);
                return Ok(GetResponse(result));
            }
            catch (EntityNotFoundException)
            {
                return Ok(new Response(false, "Bus not found"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Logs out a bus with the specified ID.
        /// </summary>
        /// <param name="busId">The ID of the bus to logout.</param>
        /// <returns>The result of the logout operation.</returns>
        [HttpGet("{busId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout(int busId)
        {
            try
            {
                var loginResult = await _bus.Logout(busId);
                return Ok(new Response(true, "Logout is Done"));
            }
            catch (Exception)
            {
                // Other error occurred
                return StatusCode(500);
            }
        }



    }
}

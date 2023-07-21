using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Exceptions;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.LineService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// <summary>
// Get Course by ID
// </summary>
// <param name="id"></param>
// <returns></returns>

namespace BusTracking.Api.Controllers
{
    public class BusController : BaseController
    {
        private readonly IBusService _bus;
        public BusController(IBusService bus)
        {
            _bus = bus;
        }


    }
}

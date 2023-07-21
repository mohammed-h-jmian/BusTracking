using BusTracking.Core.Dtos.StopPointDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.CityService;
using BusTracking.Infrastructure.Services.LineStopPointService;
using BusTracking.Infrastructure.Services.StopPointService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class StopPointController : BaseController
    {
        private readonly IStopPointService _stopPoint;
        private readonly ICityService _city;

        public StopPointController(IUserService user, IStopPointService stopPoint,
            ICityService city) : base(user)
        {
            _stopPoint = stopPoint;
            _city = city;
        }
        // GET: StopPointController
        public async Task<ActionResult> Index()
        {
            var cities = await _city.GetAll();
            var citySelectList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CitySelectList = citySelectList;
            var compositeVM = new StopPointCompositeViewModel
            {
                ListStopPoints = await _stopPoint.GetAll()
            };
            return View(compositeVM);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var stopPoints = await _stopPoint.GetAll();
            return Json(stopPoints);
        }

        // GET: StopPointController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var stopPoint = await _stopPoint.Get(id);

            if (stopPoint == null)
            {
                return RedirectToAction("NotFound", "Base");
            }
            var cities = await _city.GetAll();
            var citySelectList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CitySelectList = citySelectList;

            var compositeVM = new StopPointCompositeViewModel
            {
                StopPoint = stopPoint
            };

            return View(compositeVM);
        }

        // POST: StopPointController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStopPointDto createDto)
        {
            if (userType != UserType.MapAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var point = await _stopPoint.Create(createDto);
                    return RedirectToAction(nameof(Index));
                    //return Ok(OperationResults.AddSuccessResult());
                }
                return View(createDto);
            }
            catch
            {
                return View(createDto);
            }
        }


        // POST: StopPointController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateStopPointDto updateDto)
        {

            if (userType != UserType.MapAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var point = await _stopPoint.Update(updateDto);

                    //ViewBag.SuccessMessage = OperationResults.AddSuccessResult() ;
                    String msg = "Edit success";

                    return Redirect("~/Admin/StopPoint/Details/" + updateDto.Id);
                }
                return Redirect("~/Admin/StopPoint/Details/" + updateDto.Id);
            }
            catch
            {
                return Redirect("~/Admin/StopPoint/Details/" + updateDto.Id);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (userType != UserType.MapAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                await _stopPoint.Delete(id);

                // Get the Referer URL from the Request headers
                string referer = Request.Headers["Referer"].ToString();

                // Redirect back to the previous page
                return Redirect(referer);
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
            }
        }
    }
}

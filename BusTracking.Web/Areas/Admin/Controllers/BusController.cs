using BusTracking.Core.Constants;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Data;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class BusController : BaseController
    {
        private readonly IBusService _bus;
        public BusController(IUserService user, IBusService bus) : base(user)
        {
            _bus = bus;
        }

        public async Task<ActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString() && (userType != UserType.CompanyAdmin.ToString() && companyStatus != Status.Activated.ToString()))
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            return View(await _bus.GetAll(CompanyId));
        }

        public async Task<ActionResult> Details(int id)
        {
            if (userType != UserType.Administrator.ToString() && userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            var bus = await _bus.Get(id);

            if (bus == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && bus.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            var compositeVM = new BusCompositeViewModel
            {
                Bus = bus
            };

            return View(compositeVM);
        }

        public async Task<ActionResult> GetBusLocation(int id)
        {
            var bus = await _bus.Get(id);

            if (bus == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }

            double latitude = (double)bus.Latitude;
            double longitude = (double)bus.Longitude;

            return Json(new { Latitude = latitude, Longitude = longitude });
        }


        [HttpGet]
        public ActionResult Create(int companyId)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBusDto createBusDto)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (createBusDto.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }


            try
            {
                if (ModelState.IsValid)
                {
                    var bus = await _bus.Create(createBusDto);
                    return Redirect("~/Admin/Base/Success/");
                }
                return View(createBusDto);
            }
            catch
            {
                return View(createBusDto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateBusDto dto)
        {
            if (userType != UserType.Administrator.ToString() && userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            try
            {
                if (ModelState.IsValid)
                {
                    var bus = await _bus.Update(dto);


                    return Redirect("~/Admin/Bus/Details/" + dto.Id);
                }
                return Redirect("~/Admin/Bus/Details/" + dto.Id);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var bus = await _bus.Get(id);

            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (bus.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            try
            {
                await _bus.Delete(id);
                string referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
            }
        }
    }
}

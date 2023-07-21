using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.LineService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using BusTracking.Core.ViewModels;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Infrastructure.Services.UserService;
using BusTracking.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class LineController : BaseController
    {

        private readonly ILineService _line;

        public LineController(IUserService user, ILineService line) : base(user)
        {
            _line = line;
        }

        // GET: LineController
        public async Task<ActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString() && userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");
            }

            var compositeVM = new LineCompositeViewModel
            {
                ListLines = await _line.GetAll(CompanyId)
            };
            return View(compositeVM);
        }

        // GET: LineController/Details/5
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

            var line = await _line.Get(id);

            if (line == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && line.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }


            var compositeVM = new LineCompositeViewModel
            {
                Line = line
            };

            return View(compositeVM);
        }

        // POST: LineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLineDto createLineDto)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var line = await _line.Create(createLineDto);
                    return Redirect("~/Admin/Company/Details/" + createLineDto.CompanyId);
                    //return Ok(OperationResuslts.AddSuccessResult());
                }
                return Redirect("~/Admin/Company/Details/" + createLineDto.CompanyId);
            }
            catch
            {
                return Redirect("~/Admin/Company/Details/" + createLineDto.CompanyId);
            }
        }


        // POST: LineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateLineDto updateDto)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var line = await _line.Update(updateDto);
                    return Redirect("~/Admin/Line/Details/" + line);
                }
                return Redirect("~/Admin/Line/Details/" + updateDto.Id);
            }
            catch
            {
                return Redirect("~/Admin/Line/Details/" + updateDto.Id);
            }
        }

        // POST: LineController/Edit/5
        [HttpGet]
        public async Task<ActionResult> GenerateCode(int id)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");
            }
            try
            {
                await _line.GenerateNewCode(id);
                return Redirect("~/Admin/Line/Details/" + id);
            }
            catch
            {
                return Redirect("~/Admin/Line/Details/" + id);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");
            }
            try
            {
                await _line.Delete(id);
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

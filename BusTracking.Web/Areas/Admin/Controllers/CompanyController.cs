using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.CompanyService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _company;
        public CompanyController(IUserService user, ICompanyService company) : base(user)
        {
            _company = company;
        }

        public async Task<IActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            return View(await _company.GetAll());
        }
        public async Task<IActionResult> Profile()
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }
            return Redirect("/Admin/Company/Details/" + CompanyId);
        }

        public IActionResult Pending()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (userType != UserType.Administrator.ToString() && userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            else if (userType != UserType.Administrator.ToString() && companyStatus != Status.Activated.ToString())
            {
                return Redirect("/Admin/Company/Pending");

            }

            var company = await _company.Get(id);

            if (company == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && company.Id != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            var compositeVM = new CompanyCompositeViewModel
            {
                Company = company
            };

            return View(compositeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            try
            {
                await _company.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCompanyDto updateDto)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            if (updateDto.Id != CompanyId)
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
                    var company = await _company.Update(updateDto);
                    return Redirect("~/Admin/Company/Details/" + updateDto.Id);
                }
                return Redirect("~/Admin/Company/Details/" + updateDto.Id);
            }
            catch
            {
                return Redirect("~/Admin/Company/Details/" + updateDto.Id);
            }
        }

        [HttpGet]
        public async Task<ActionResult> SetPending(int id)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                await _company.Pending(id);
                string referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);
                //return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
            }
        }

        [HttpGet]
        public async Task<ActionResult> SetActive(int id)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                await _company.Active(id); 
                string referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);

                //return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
            }
        }
    }
}

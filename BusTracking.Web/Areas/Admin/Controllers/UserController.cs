using BusTracking.Core.Constants;
using BusTracking.Core.Dtos;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.UserViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.CityService;
using BusTracking.Infrastructure.Services.CompanyService;
using BusTracking.Infrastructure.Services.EmailService;
using BusTracking.Infrastructure.Services.UserService;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Graph.Models;
using Tavis.UriTemplates;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _user;
        private readonly ICityService _city;

        public UserController(IUserService user, ICityService city) : base(user)
        {
            _user = user;
            _city = city;
        }

        public async Task<ActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            var users = await _user.GetAll();
            var composite = new UserCompositeViewModel
            {
                Users = users,
            };

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                TempData.Remove("SuccessMessage");
            }
            else if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                TempData.Remove("ErrorMessage");
            }

            return View(composite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateUserDto updateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    updateDto.UpdatedBy = userId;
                    updateDto.UpdatedAt = DateTime.Now;
                    var user = await _user.Update(updateDto);
                    TempData["SuccessMessage"] = "The modification was completed successfully";
                    return RedirectToAction("Details", new { id = updateDto.Id });
                }
                TempData["ErrorMessage"] = "Failed to update. left blank values";
                return RedirectToAction("Details", new { id = updateDto.Id });
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to update. Please try again later.";
                return RedirectToAction("Details", new { id = updateDto.Id });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {

            if (userType == UserType.CompanyAdmin.ToString() && companyStatus != Core.Enums.Status.Activated.ToString())
            {
                return base.Redirect("/Admin/Company/Pending");

            }

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                TempData.Remove("SuccessMessage");
            }
            else if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                TempData.Remove("ErrorMessage");
            }


            var user = await _user.Get(id);

            if (user == null)
            {
                return RedirectToAction("NotFound", "Base");
            }
            else if (userType == UserType.CompanyAdmin.ToString() && user.Company.Id != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            var userVM = new UserCompositeViewModel
            {
                User = user
            };

            return View(userVM);
        }
        [HttpGet]
        public async Task<ActionResult> NewPassword(string id)
        {
            if (userId != id)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {

                var user = await _user.NewPassword(id);
                TempData["SuccessMessage"] = "Your new password has been sent to your email";
                return RedirectToAction("Details", new { id = id });

            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to update the password. Please try again later.";
                return RedirectToAction("Details", new { id = id });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditType(UpdateUserTypeDto dto)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                dto.UpdatedBy = userId;
                dto.UpdatedAt = DateTime.Now;

                var user = await _user.EditType(dto);
                TempData["SuccessMessage"] = $"User type changed as {dto.UserType.ToString()} successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while changing the user type";
                return RedirectToAction("Index");
            }
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserDto createDto)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {

                createDto.CreatedBy = userId;
                var result = await _user.Create(createDto);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"The user has been added {createDto.FirstName} {createDto.LastName} successfully, The login information has been sent to his email";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                string modelStateErrors = string.Join(". ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData["ErrorMessage"] = $"Failed to add user {createDto.FirstName} {createDto.LastName}. {modelStateErrors}. Try again later";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["ErrorMessage"] = $"Failed to add user {createDto.FirstName} {createDto.LastName}. Try again later ";
                return RedirectToAction("Index");
            }
        }

        // GET: UserController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            try
            {
                await _user.Delete(id);
                TempData["SuccessMessage"] = "The deletion was completed successfully";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred during the deletion process.";
                return RedirectToAction("Index");
            }
        }

        // GET: User/Register
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(DefaultURL.GetDefaultURL(userType));
            }

            var cities = await _city.GetAll();

            var citySelectList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CitySelectList = citySelectList;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(DefaultURL.GetDefaultURL(userType));
            }

            if (ModelState.IsValid)
            {
                var result = await _user.Register(dto);

                if (result.Succeeded)
                {
                    var loginDto = new LoginDto
                    {
                        Email = dto.UserDto.Email,
                        Password = dto.UserDto.Password,
                    };
                    await _user.Login(loginDto);
                    return Redirect("/Admin/Company/Pending");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var cities = await _city.GetAll();

            var citySelectList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CitySelectList = citySelectList;
            return View(dto);
        }

        // GET: User/Login?returnUrl=
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/Admin")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(DefaultURL.GetDefaultURL(userType));
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto, string returnUrl = "/Admin")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(DefaultURL.GetDefaultURL(userType));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _user.Login(dto);

                    if (result.Succeeded)
                    {
                        var userType = await _user.GetUserType(dto.Email);

                        if (userType == UserType.Administrator)
                        {
                            return Redirect("/Admin");
                        }
                        else if (userType == UserType.CompanyAdmin)
                        {
                            if (CompanyId != 0)
                            {
                                return Redirect("/Admin/Company/Details" + CompanyId);
                            }
                        }
                        else if (userType == UserType.MapAdmin)
                        {
                            return Redirect("/Admin/StopPoint");
                        }
                        else
                        {
                            return Redirect("/Admin");
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt");

                }
                catch (Exception)
                {

                    return Redirect("/Admin/User/Login");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var result = await _user.Logout();

            if (result.Succeeded)
            {
                return Redirect("/Admin/User/Login");
            }

            return RedirectToAction("Error", "Home");
        }
    }
}

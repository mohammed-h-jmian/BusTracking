using BusTracking.Core.Constants;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.UserViewModels;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph.Models;
using System;
using System.Security.Claims;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BaseController : Controller
    {
        private readonly IUserService _user;
        protected string userType;
        protected string userId;
        protected int CompanyId;
        protected string companyStatus = "";
        protected int userTypeNum;

        public BaseController(IUserService user)
        {
            _user = user;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var userName = User.Identity.Name;
                    var user = _user.GetUserByUsername(userName);


                    if (user.Company != null)
                    {
                        companyStatus = user.Company.Status.ToString();
                    }

                    if (user != null)
                    {
                        userType = user.UserType.ToString();
                        userTypeNum = (int)user.UserType;
                        CompanyId = user.CompanyId;
                        userId = user.Id;
                        ViewBag.FullName = user.FullName;
                        ViewBag.UserId = user.Id;
                        ViewBag.ImageUrl = user.ImageUrl;
                        ViewBag.CompanyId = user.CompanyId;
                        ViewBag.UserType = user.UserType.ToString();
                        if (user.UserType.ToString() == UserType.CompanyAdmin.ToString())
                        {
                            ViewBag.CompanyName = user.Company.Name;
                        }
                    }
                    else
                    {
                        Redirect("/Base/DeleteUser");
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        public IActionResult NotFound()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult DeleteUser()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }
        public IActionResult Unauthorized()
        {
            return View();
        }
        public IActionResult DefaultPage()
        {
            return Redirect(DefaultURL.GetDefaultURL(userType));
        }
    }
}

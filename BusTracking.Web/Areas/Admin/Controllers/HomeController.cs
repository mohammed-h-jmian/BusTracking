using BusTracking.Core.Constants;
using BusTracking.Core.Enums;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.CompanyService;
using BusTracking.Infrastructure.Services.DashboardService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDashboardService _dashboard;

        public HomeController(IUserService user, IDashboardService dashboard) : base(user)
        {
            _dashboard = dashboard;
        }

        public async Task<IActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString())
            {
                return Redirect(DefaultURL.GetDefaultURL(userType));
            }

            var data = await _dashboard.GetData();
            return View(data);
        }
    }
}

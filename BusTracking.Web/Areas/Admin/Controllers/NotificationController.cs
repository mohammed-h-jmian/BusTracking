using BusTracking.Core.Enums;
using BusTracking.Infrastructure.Services.NotificationService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notification;

        public NotificationController(IUserService user,
            INotificationService notification) : base(user)
        {
            _notification = notification;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (userType == UserType.Administrator.ToString())
            {
                var notifications = await _notification.GetAll();
                return Json(notifications);
            }
            else if (userType == UserType.CompanyAdmin.ToString())
            {
                var notifications = await _notification.GetAll(CompanyId);
                return Json(notifications);
            }

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetLast()
        {
            if (userType == UserType.Administrator.ToString())
            {
                var notifications = await _notification.GetLast();
                return Json(notifications);
            }
            else if (userType == UserType.CompanyAdmin.ToString())
            {
                var notifications = await _notification.GetAll(CompanyId);
                return Json(notifications);
            }

            return Ok();
        }
        public async Task<IActionResult> Index()
        {
            if (userType != UserType.Administrator.ToString() && (userType != UserType.CompanyAdmin.ToString() && companyStatus != Status.Activated.ToString()))
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            return View();
        }
    }

}

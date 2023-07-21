using Microsoft.AspNetCore.Mvc;

namespace BusTracking.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult DeleteUser()
        {
            return View();
        }

    }
}

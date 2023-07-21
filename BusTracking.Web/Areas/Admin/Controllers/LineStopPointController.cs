using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.Enums;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.CompanyService;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.LineStopPointService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tavis.UriTemplates;

namespace BusTracking.Web.Areas.Admin.Controllers
{
    public class LineStopPointController : BaseController
    {
        private readonly ILineStopPointService _lineStopPoint;
        private readonly ILineService _line;

        public LineStopPointController(IUserService user,
            ILineStopPointService lineStopPoint,
            ILineService line) : base(user)
        {
            _lineStopPoint = lineStopPoint;
            _line = line;
        }

        // GET: LineStopPointController/Details/5
        public async Task<ActionResult> Details(int lineId)
        {
            try
            {
                ViewBag.lineId = lineId;
                var result = await _lineStopPoint.GetAllByLineId(lineId);
                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Base");
                throw;
            }
        }

        // GET: LineStopPointController/Create
        public async Task<ActionResult> Create(int lineId)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            var line = await _line.Get(lineId);

            if (line == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && line.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                ViewBag.lineId = lineId;
                var result = await _lineStopPoint.GetAllByLineId(lineId);
                return View(result);
            }
            catch (Exception)
            {
                return Redirect("/Admin/Base/NotFound");
                throw;
            }

        }

        // POST: LineStopPointController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLineStopPointDto createDto)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            var line = await _line.Get(createDto.LineId);

            if (line == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && line.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }

            string referer = Request.Headers["Referer"].ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    var lineStopPoint = await _lineStopPoint.Create(createDto);
                    return Redirect(referer);
                    //return Ok(OperationResults.AddSuccessResult());
                }
                return Redirect(referer);
            }
            catch
            {
                return Redirect(referer);
            }
        }


        // GET: LineStopPointController/Get
        public async Task<ActionResult> Get(int lineId)
        {
            try
            {
                var result = await _lineStopPoint.GetAllByLineId(lineId);
                return Json(result);
            }
            catch (Exception ex)
            {
                // Example: Returning a custom error message
                return StatusCode(500, "An error occurred while retrieving line stop points. Please try again later.");
            }
        }

        // GET: LineStopPointController/Edit/5
        public ActionResult Edit(int id , int order)
        {
            ViewBag.Id = id;
            ViewBag.Order = order;
            return View();
        }

        // POST: LineStopPointController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (userType != UserType.CompanyAdmin.ToString())
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            var lineStopPoint = await _lineStopPoint.Get(id);
            if (lineStopPoint == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }

            var line = await _line.Get(lineStopPoint.LineId);

            if (line == null)
            {
                return Redirect("/Admin/Base/NotFound");
            }
            else if (userType != UserType.Administrator.ToString() && line.CompanyId != CompanyId)
            {
                return Redirect("/Admin/Base/Unauthorized");
            }
            try
            {
                await _lineStopPoint.Delete(id);


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

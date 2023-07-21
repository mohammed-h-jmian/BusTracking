using BusTracking.Core.Dtos;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Infrastructure.Services.CityService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusTracking.Web.Controllers
{
    public class UserController : BaseController
    {
        //private readonly IUserService _user;
        //private readonly ICityService _city;
        //public UserController(IUserService user, ICityService city)
        //{
        //    _user = user;
        //    _city = city;
        //}
        //// GET: User/Register
        //public async Task<ActionResult> Register()
        //{
        
        //    var cities = await _city.GetAll();

        //    var citySelectList = cities.Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.Name
        //    }).ToList();

        //    ViewBag.CitySelectList = citySelectList;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterDto dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _user.Register(dto);

        //        if (result.Succeeded)
        //        {
        //            var loginDto = new LoginDto
        //            {
        //                Email = dto.UserDto.Email,
        //                Password = dto.UserDto.Password,
        //            };
        //            await _user.Login(loginDto);
        //            return Redirect("/Admin/Company/Pending");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }
        //    var cities = await _city.GetAll();

        //    var citySelectList = cities.Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.Name
        //    }).ToList();

        //    ViewBag.CitySelectList = citySelectList;
        //    return View(dto);
        //}

        //// GET: User/Login?returnUrl=
        //[HttpGet]
        //public IActionResult Login(string returnUrl = "/Admin")
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDto dto, string? returnUrl = "/Admin")
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _user.Login(dto);

        //        if (result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid login attempt");
        //    }

        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View(dto);
        //}


    }
}

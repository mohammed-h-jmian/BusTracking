using BusTracking.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Middlewares
{
    public class CheckUserDeletedMiddleware
    {
        private readonly RequestDelegate _next;
    

        public CheckUserDeletedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.User.Identity.IsAuthenticated)
            {

                var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.GetUserAsync(context.User);

                if (user != null && user.IsDelete)
                {
                    // Check if the current request is already for the logout page
                    if (context.Request.Path == "/Admin/User/Logout")
                    {
                        await _next(context);
                        return;
                    }

                    context.Response.Redirect("/Admin/User/Logout");
                    return;
                }
            }

            await _next(context);
        }
    }
}

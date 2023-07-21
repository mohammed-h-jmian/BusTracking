using BusTracking.Data;
using Microsoft.EntityFrameworkCore;
using BusTracking.Infrastructure.Extentions;
using BusTracking.Infrastructure.AutoMappers;
using BusTracking.Data.Models;
using Microsoft.AspNetCore.Identity;
using BusTracking.Core.Enums;
using BusTracking.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BusDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services
    .AddConfig(builder.Configuration)
    .RegisterServices();


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BusDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = false; // Set to false to remove the requirement for symbols
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 8; // Change the minimum password length here
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/User/Login";
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        DbSeeder.SeedDatabaseAsync(serviceProvider).Wait();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.InnerException?.Message);
        throw;
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckUserDeletedMiddleware>();

app.UseStatusCodePagesWithReExecute("/Base/NotFound");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapRazorPages();

app.Run();
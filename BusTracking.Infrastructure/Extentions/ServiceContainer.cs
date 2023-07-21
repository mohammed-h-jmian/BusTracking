using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.StopPointService;
using BusTracking.Infrastructure.Services.CompanyService;
using Microsoft.Extensions.Configuration;
using BusTracking.Infrastructure.Services.FileService;
using BusTracking.Infrastructure.Services.LineStopPointService;
using BusTracking.Infrastructure.Services.DashboardService;
using BusTracking.Infrastructure.Services.EmailService;
using BusTracking.Infrastructure.Services.UserService;
using BusTracking.Infrastructure.Services.CityService;
using BusTracking.Infrastructure.Services.NotificationService;

namespace BusTracking.Infrastructure.Extentions
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddConfig(
        this IServiceCollection services, IConfiguration config)
        {

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBusService, BusService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddTransient<ILineService, LineService>();
            services.AddTransient<IStopPointService, StopPointService>();
            services.AddTransient<ILineStopPointService, LineStopPointService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<INotificationService, NotificationService>();


            return services;
        }
    }
}

using BusTracking.Data;
using BusTracking.Infrastructure.AutoMappers;
using BusTracking.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BusTracking.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BusDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services
    .AddConfig(builder.Configuration)
    .RegisterServices();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://mohammedjmain-001-site1.atempurl.com/",
        ValidAudience = "http://mohammedjmain-001-site1.atempurl.com/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jgghhkiderhvbNMfghjgjgghhkiderhvbNMfghjg"))
    };
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
    });


builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Change the response when authentication fails
            context.Response.StatusCode = 401;
            context.HandleResponse();
            context.Response.ContentType = "application/json";

            // Customize the JSON response
            var errorResponse = new
            {
                status = false,
                message = "Access denied. You do not have permission to access this method."
            };

            // Serialize the JSON response
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            // Write the JSON response to the response body
            return context.Response.WriteAsync(jsonResponse);
        }
    };
});



builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BusDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusTracking", Version = "v1" });

    // Define the JWT bearer scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Apply the JWT bearer scheme to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    //    app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
             name: "Driver",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
});




app.Run();

using BusTracking.Core.Enums;
using BusTracking.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<BusDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    await SeedCitiesAsync(context);
                    await SeedDefaultUserAsync(userManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }


        private static async Task SeedCitiesAsync(BusDbContext context)
        {
            if (await context.Cities.AnyAsync())
            {
                return;
            }

            var cities = new List<City>
        {
            new City
            {
                Name = "Rafah",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Gaza",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Khan Yunis",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Jabalia",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Beit Lahia",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Deir al-Balah",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Al-Mughraqa",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Beit Hanoun",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },
            new City
            {
                Name = "Nuseirat",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },     
            new City
            {
                Name = "Al-Bureij",
                CreatedAt = DateTime.Now,
                CreatedBy = "default"
            },

        };

            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }


        private static async Task SeedDefaultUserAsync(UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var user = new User
            {
                FullName = "System Developer",
                UserName = "dev@gmail.com",
                Email = "dev@gmail.com",
                CreatedAt = DateTime.Now,
                CreatedBy = "default",
                EmailConfirmed = true,
                UserType = UserType.Administrator
            };

            await userManager.CreateAsync(user, "$Admin$1234$");
        }

    }
}

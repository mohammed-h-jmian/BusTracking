using BusTracking.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data
{
    public class BusDbContext : IdentityDbContext<User>
    {
        public BusDbContext(DbContextOptions<BusDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineStopPoint> LineStopPoints { get; set; }
        public DbSet<StopPoint> StopPoints { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}

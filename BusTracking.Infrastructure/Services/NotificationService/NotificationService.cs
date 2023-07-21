using AutoMapper;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;
        public NotificationService(BusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<int> Create(int? to, string subject, string text)
        {
            var notification = new Notification
            {
                Subject = subject,
                Text = text,
                CompanyId = to,
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification.Id;
        }
        public async Task<List<NotificationViewModel>> GetAll(int? companyId = 0)
        {
            var query = _context.Notifications
                .Include(x => x.Company);

            if (companyId != 0)
            {
                query = query.Where(x => x.CompanyId == companyId)
                    .Include(x => x.Company);
            }

            var notifications = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var notificationViewModels = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notifications);
            return notificationViewModels;
        }

        public async Task<List<NotificationViewModel>> GetLast(int? companyId = 0)
        {
            var query = _context.Notifications.Where(x => !x.IsRead).Include(x => x.Company);

            if (companyId != 0)
            {
                query = query.Where(x => x.CompanyId == companyId).Include(x => x.Company);
            }

            var notifications = await query
                .OrderByDescending(x => x.CreatedAt)
                .Take(3)
                .ToListAsync();

            var notificationViewModels = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notifications);
            return notificationViewModels;
        }

    }
}

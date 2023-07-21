using BusTracking.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetAll(int? companyId = 0);
        Task<int> Create(int? to, string subject, string text);
        Task<List<NotificationViewModel>> GetLast(int? companyId = 0);
    }
}

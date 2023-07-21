using BusTracking.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetData();
    }
}

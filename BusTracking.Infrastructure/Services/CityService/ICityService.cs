using BusTracking.Core.ViewModels.CityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.CityService
{
    public interface ICityService
    {
        Task<List<CityViewModel>> GetAll();
    }
}

using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels
{
    public class DashboardViewModel
    {
        public int NumberOfCompany { get; set; }
        public int NumberOfBus { get; set; }
        public int NumberOfStopPoint { get; set; }
        public int NumberOfLine { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
        public List<BusViewModel> Buses { get; set; }
        public List<LineViewModel> Lines { get; set; }
        public List<StopPointViewModel> StopPoints { get; set; }
    }
}

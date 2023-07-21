using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.CityViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.StopPointViewModels
{
    public class StopPointViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public CityViewModel City { get; set; }
        public List<LineStopPointViewModel> LinesSP { get; set; }
    }
}

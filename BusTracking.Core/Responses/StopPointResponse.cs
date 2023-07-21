using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.CityViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class StopPointResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public CityResponse City { get; set; }
        public List<LinesStopPointResponse> LineSP { get; set; }

        public StopPointResponse()
        {
            LineSP = new List<LinesStopPointResponse>(); // Initialize the list
        }
    }
}

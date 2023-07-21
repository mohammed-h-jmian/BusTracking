using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.Responses.CompanyResponses;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class BusLineResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusId { get; set; }
        public BusResponse Bus { get; set; }
        public List<StopPointsLineResponse> LineSP { get; set; }

        public BusLineResponse()
        {
            LineSP = new List<StopPointsLineResponse>(); // Initialize the list
        }
    }
}

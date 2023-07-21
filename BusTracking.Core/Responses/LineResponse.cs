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
    public class LineResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public CompanyResponse Company { get; set; }

    }
}

using BusTracking.Core.Responses.CompanyResponses;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses.BusResponses
{
    public class BusResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }
        public CompanyResponse? Company { get; set; }
        public string? ImgPath { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
      
    }
}

using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CityViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses.CompanyResponses
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public int? CityId { get; set; }
        public CityResponse City { get; set; }
    }
}

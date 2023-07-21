using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CityViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;

namespace BusTracking.Core.ViewModels.CompanyViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public List<LineViewModel> Lines { get; set; }
        public List<BusViewModel> Buses { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public int? CityId { get; set; }
        public CityViewModel City { get; set; }
        public Status Status { get; set; }
    }
}

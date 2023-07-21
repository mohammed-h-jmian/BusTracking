using BusTracking.Core.ViewModels.CompanyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.CityViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
    }
}

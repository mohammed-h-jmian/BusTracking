using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;

namespace BusTracking.Core.ViewModels.LinesViewModels
{
    public class LineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public CompanyViewModel Company { get; set; }
        public DateTime ActiveExpired { get; set; }
        public int? BusId { get; set; }
        public BusViewModel Bus { get; set; }
        public string Code { get; set; }
        public List<LineStopPointViewModel> LinesSP { get; set; }


    }
}

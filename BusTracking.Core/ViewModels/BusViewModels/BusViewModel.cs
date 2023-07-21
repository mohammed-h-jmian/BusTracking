using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;

namespace BusTracking.Core.ViewModels.BusViewModels
{
    public class BusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }
        public CompanyViewModel? Company { get; set; }
        public string? Password { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsActive { get; set; }
        public string? ImgPath { get; set; }
        public LineViewModel Line { get; set; }

    }
}


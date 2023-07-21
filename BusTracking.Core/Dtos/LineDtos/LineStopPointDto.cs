using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.LineDtos
{
    public class LineStopPointDto
    {
        [Required(ErrorMessage = "The Stop Point field is required.")]
        [Display(Name = "Stop Point")]
        public int StopPointId { get; set; }
        [Required(ErrorMessage = "The Time Access field is required.")]
        [Display(Name = "Time Access")]
        public string TimeAccess { get; set; }
    }
}

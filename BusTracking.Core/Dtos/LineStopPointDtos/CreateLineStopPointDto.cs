using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.LineStopPointDtos
{
    public class CreateLineStopPointDto
    {

        [Required(ErrorMessage = "The Line ID field is required.")]
        public int LineId { get; set; }
        [Required(ErrorMessage = "The Stop Point Id field is required.")]
        public int StopPointId { get; set; }
        [Required(ErrorMessage = "The Time Access field is required.")]
        [Display(Name = "Time Access")]
        public string TimeAccess { get; set; }
        [Required(ErrorMessage = "The Stop Order field is required.")]
        [Display(Name = "Order")]
        public int Order { get; set; }

    }
}

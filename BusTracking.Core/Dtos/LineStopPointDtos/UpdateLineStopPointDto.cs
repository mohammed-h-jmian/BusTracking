using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.LineStopPointDtos
{
    public class UpdateLineStopPointDto
    {
        [Required(ErrorMessage = "The Line Stop Point ID field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Time Access field is required.")]
        [Display(Name = "Time Access")]
        public string TimeAccess { get; set; }
        [Required(ErrorMessage = "The Stop Order field is required.")]
        [Display(Name = "Order")]
        public int Order { get; set; }
    }
}

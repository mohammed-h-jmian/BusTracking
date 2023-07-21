using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.LineDtos
{
    public class UpdateLineDto
    {
        [Required(ErrorMessage = "The Line ID field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Line Name field is required.")]
        [Display(Name = "Line Name")]
        public string Name { get; set; }

    }
}

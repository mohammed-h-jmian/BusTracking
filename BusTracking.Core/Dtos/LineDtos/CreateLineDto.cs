using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.LineDtos
{
    public class CreateLineDto
    {
        [Required(ErrorMessage = "The Line Name field is required.")]
        [Display(Name = "Line Name")]
        public string Name { get; set; }
        [Required]
        public int CompanyId { get; set; }


    }
}

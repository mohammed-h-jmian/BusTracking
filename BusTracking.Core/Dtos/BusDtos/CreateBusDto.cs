using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.BusDtos
{
    public class CreateBusDto
    {
        [Required(ErrorMessage = "The Bus Name field is required.")]
        [Display(Name = "Bus Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Bus Number field is required.")]
        [Display(Name = "Bus Number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "An Image file must be provided.")]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        public int CompanyId { get; set; }
    }
}

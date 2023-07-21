using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.BusDtos
{
    public class LoginBusDto
    {
        [Required(ErrorMessage = "Number is required.")]
        [Display(Name = "Number")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}

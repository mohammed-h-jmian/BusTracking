using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.CompanyDtos
{
    public class UpdateCompanyDto
    {
        [Required(ErrorMessage = "The Company ID field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Company Name field is required.")]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Company Logo")]
        public IFormFile? Logo { get; set; }
        [Required(ErrorMessage = "The Company Address field is required.")]
        [Display(Name = "Company Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The Company Phone Number field is required.")]
        [Display(Name = "Company Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Company Facebook")] 
        public string Facebook { get; set; }
    }
}

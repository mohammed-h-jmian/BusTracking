using BusTracking.Core.Enums;
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
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Logo is required.")]
        [Display(Name = "Logo")]
        public IFormFile Logo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(100, ErrorMessage = "Phone number must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Facebook URL is required.")]
        [StringLength(500, ErrorMessage = "Facebook URL must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Url)]
        [Display(Name = "Facebook")]
        public string Facebook { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public int CityId { get; set; }
    }

}

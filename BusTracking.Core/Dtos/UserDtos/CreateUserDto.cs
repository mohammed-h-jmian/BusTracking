using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? FullName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "The Name (2 - 20) characters", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName  { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The Name (2 - 20) characters", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public string PhoneNumber { get; set; }
        public int? CompanyId { get; set; }
        public string CreatedBy { get; set; }   

        public CreateUserDto()
        {
            UserType = UserType.CompanyAdmin;
            FullName =  " " ;
        }

    }
}

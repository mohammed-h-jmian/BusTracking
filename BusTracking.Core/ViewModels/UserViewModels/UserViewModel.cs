using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.CompanyViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        [Required]
        public string? Id { get; set; }  
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }
        public string? FCMToken { get; set; }
        public bool IsDelete { get; set; }
        public int CompanyId { get; set; }
        public CompanyViewModel Company { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}

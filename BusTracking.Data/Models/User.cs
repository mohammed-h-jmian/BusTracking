using BusTracking.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }
        public string? FCMToken { get; set; }
        public bool IsDelete { get; set; }   
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }


        public User()
        {
            CreatedAt = DateTime.Now;
        }

    }
}

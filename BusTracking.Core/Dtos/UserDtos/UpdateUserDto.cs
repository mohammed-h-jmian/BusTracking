using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The Name (2 - 100) characters", MinimumLength = 10)]
        [DataType(DataType.Text)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

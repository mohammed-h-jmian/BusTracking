using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.UserDtos
{
    public class UpdateUserTypeDto
    {
        public string UserId { get; set;}
        public UserType UserType  { get; set;}
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

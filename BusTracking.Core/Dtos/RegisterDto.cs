using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos
{
    public class RegisterDto
    {
        public CreateUserDto UserDto { get; set; }
        public CreateCompanyDto CompanyDto { get; set; }
    }
}

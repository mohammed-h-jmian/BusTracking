using BusTracking.Core.Dtos.StopPointDtos;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Core.ViewModels.StopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.UserViewModels
{
    public class UserCompositeViewModel
    {
        public CreateUserDto CreateDto { get; set; }
        public UpdateUserDto UpdateDto { get; set; }
        public List<UserViewModel> Users { get; set; }
        public UserViewModel User { get; set; }
    }
}

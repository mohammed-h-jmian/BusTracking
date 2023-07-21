using BusTracking.Core.Dtos;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.UserService
{
    public interface IUserService
    {
        Task<IdentityResult> Create(CreateUserDto dto);
        Task<IdentityResult> Register(RegisterDto dto);
        Task<SignInResult> Login(LoginDto dto);
        Task<IdentityResult> Logout();
        UserViewModel GetUserByUsername(string username);
        Task<UserType> GetUserType(string email);
        Task<List<UserViewModel>> GetAll();
        Task<UserViewModel> Get(string id);
        Task<string> Delete(string id);
        Task<string> Update(UpdateUserDto dto);
        Task<string> EditType(UpdateUserTypeDto dto);
        Task<string> NewPassword(string id);
    }
}
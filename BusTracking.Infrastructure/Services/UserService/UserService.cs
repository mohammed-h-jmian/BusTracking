using AutoMapper;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Core.Dtos;
using BusTracking.Data;
using BusTracking.Infrastructure.Services.CompanyService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Identity;
using BusTracking.Data.Models;
using Microsoft.EntityFrameworkCore;
using BusTracking.Core.Exceptions;
using BusTracking.Core.ViewModels.UserViewModels;
using BusTracking.Core.Enums;
using BusTracking.Core.ViewModels.LinesViewModels;
using Org.BouncyCastle.Utilities;
using BusTracking.Core.ViewModels.CompanyViewModels;
using System.Numerics;
using BusTracking.Core.Constants;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Infrastructure.Services.FileService;
using BusTracking.Infrastructure.Services.EmailService;
using System.Text;

namespace BusTracking.Infrastructure.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;
		private readonly BusDbContext _context;
		private readonly ICompanyService _company;
		private readonly IFileService _file;
		private readonly IEmailService _email;

		public UserService(UserManager<User> userManager,
						   SignInManager<User> signInManager,
						   IMapper mapper,
						   BusDbContext context,
						   ICompanyService company,
						   IFileService file,
						   IEmailService email)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_context = context;
			_company = company;
			_file = file;
			_email = email;
		}
		public async Task<List<UserViewModel>> GetAll()
		{
			var users = await _context.Users
				.Include(x => x.Company)
				 .Where(x => !x.IsDelete).ToListAsync();

			var usersVM = _mapper.Map<List<User>, List<UserViewModel>>(users);
			return usersVM;
		}
		public async Task<UserViewModel> Get(string id)
		{
			var user = await _context.Users
				.Include(x => x.Company)
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);


			var userVM = _mapper.Map<UserViewModel>(user);
			return userVM;
		}
		public UserViewModel GetUserByUsername(string username)
		{
			var user = _context.Users
				.Include(x => x.Company)
				.SingleOrDefault(x => x.UserName == username && !x.IsDelete);
			if (user == null)
			{
				throw new EntityNotFoundException();
			}
			return _mapper.Map<UserViewModel>(user);
		}
		public async Task<IdentityResult> Register(RegisterDto dto)
		{
			var companyId = await _company.Create(dto.CompanyDto);

			if (companyId == 0)
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "ErrorCreateCompany",
					Description = "Error creating company"
				});
			}

			dto.UserDto.CompanyId = companyId;

			var result = await Create(dto.UserDto);

			if (result.Succeeded)
			{
				var adminUsers = await _context.Users
					.Where(x => !x.IsDelete && x.UserType == UserType.Administrator)
					.ToListAsync();

				foreach (var user in adminUsers)
                {
                    string subject = "New Company Registration";
                    string body = $@"

Dear {dto.UserDto.FirstName},

We hope this email finds you well. We are excited to inform you that your company, {dto.CompanyDto.Name}, has been successfully registered on our platform. Congratulations on taking this step!

However, we would like to inform you that your company's profile is currently in ""Pending Review"" status. Our team is working diligently to review all submitted company profiles, ensuring the best experience for our users.

What's next?
While your company's profile is under review, you can make sure it stands out by providing detailed and accurate information. A well-completed profile will increase your chances of approval.

Once your profile is approved, it will be visible to other users, expanding your business opportunities on our platform.

If you have any questions or need assistance during the process, don't hesitate to reach out to our support team at support@example.com. We are here to help!

Thank you for joining our community. We look forward to seeing your company thrive on our platform.

Best regards,
Bus Tracking
";
					await _email.Send(user.Email, subject, body);
				}
				return result;
			}
			else
			{
				await _company.Remove(companyId);
				return result;
			}
		}
		public async Task<IdentityResult> Create(CreateUserDto dto)
		{
			var emailOrPhoneExists = await _context.Users
				.AnyAsync(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));

			if (emailOrPhoneExists)
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "EmailOrPhoneExists",
					Description = "Email or phone number already exists."
				});
			}

			var password = GenerateRandomPassword();

			var user = _mapper.Map<CreateUserDto, User>(dto);
			user.FullName = $"{dto.FirstName} {dto.LastName}";
			user.EmailConfirmed = true;
			user.UserName = dto.Email;



			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				string subject = "Welcome to Our System!";
				string body = $"Dear {user.FullName},\n\n" +
							  $"Welcome to our system! We are delighted to have you as a {user.UserType.ToString()}. This email contains important information regarding your login credentials and instructions on how to access the system.\n\n" +
							  $"Username: {user.Email}\n" +
							  $"Password: {password}\n\n" +
                              "Please keep this information secure and do not share it with anyone. If you have any questions or need assistance, feel free to reach out to our support team at bustracking4563069@gmail.com .\n\n" +
							  "Once again, welcome aboard! We hope you have a great experience using our system.\n\n" +
							  "Best regards,\n" +
							  "[Admin]\n" +
							  "[Bus Tracking]";

				await _email.Send(user.Email, subject, body);
			}
			return result;
		}
		public async Task<SignInResult> Login(LoginDto dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);

			if (user == null)
			{
				return SignInResult.Failed;
			}

			if (user.IsDelete)
			{
				return SignInResult.LockedOut;
			}

			var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutOnFailure: false);
			return result;
		}
		public async Task<IdentityResult> Logout()
		{
			await _signInManager.SignOutAsync(); // Sign out the user
			return IdentityResult.Success;
		}
		public async Task<UserType> GetUserType(string email)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

			if (user == null)
			{
				throw new EntityNotFoundException();
			}

			return user.UserType;
		}
		public async Task<string> Delete(string id)
		{
			var user = await _context.Users
				.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

			if (user == null)
			{
				throw new EntityNotFoundException();
			}

			user.IsDelete = true;
			await _context.SaveChangesAsync();
			return user.Id;
		}
		public async Task<string> Update(UpdateUserDto dto)
		{

			var user = await _context.Users
				.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);

			if (user == null)
			{
				throw new EntityNotFoundException();
			}

			if (dto.ImageUrl != null)
			{
				user.ImageUrl = await _file.SaveFile(dto.ImageUrl, "UserImg");
				user.ImageUrl = Paths.baseUrl + Paths.UserImgPath + user.ImageUrl;
			}

			_mapper.Map(dto, user);
			await _context.SaveChangesAsync();
			return user.Id;
		}
		public async Task<string> EditType(UpdateUserTypeDto dto)
		{

			var user = await _context.Users
				.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.UserId);

			if (user == null)
			{
				throw new EntityNotFoundException();
			}
			user.UserType = dto.UserType;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return user.Id;
		}
        public async Task<string> NewPassword(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            // Generate a new password
            string newPassword = GenerateRandomPassword();

            // Update the user's password
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, newPassword);

            await _context.SaveChangesAsync();

            string subject = "New Password";
            string body = $"Dear {user.FullName},\n\n" +
                          "Your password has been reset. Please use the following new password to log in:\n\n" +
                          $"New Password: {newPassword}\n\n" +
                          "Please ensure that you change your password after logging in for security purposes.\n\n" +
                          "Best regards,\n" +
                          "[Admin]\n" +
                          "[Bus Tracking]";
            await _email.Send(user.Email, subject, body);


            return newPassword;
        }
        private string GenerateRandomPassword()
		{
			string symbols = "$#@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			Random random = new Random();
			StringBuilder sb = new StringBuilder();

			// Generate a random symbol of length 10
			for (int i = 0; i < 10; i++)
			{
				int index = random.Next(symbols.Length);
				sb.Append(symbols[index]);
			}

			return sb.ToString();
		}
	}
}
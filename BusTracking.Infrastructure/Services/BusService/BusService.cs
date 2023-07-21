using AutoMapper;
using BusTracking.Core.Constants;
using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.Exceptions;
using BusTracking.Core.Responses;
using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.EmailService;
using BusTracking.Infrastructure.Services.FileService;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.LineStopPointService;
using BusTracking.Infrastructure.Services.NotificationService;
using BusTracking.Infrastructure.Services.StopPointService;
using BusTracking.Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BusTracking.Infrastructure.Services.BusService
{
    public class BusService : IBusService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _file;
        private readonly ILineService _line;
        private readonly IStopPointService _stop;
        private readonly ILineStopPointService _lineSP;
        private readonly INotificationService _notification;
        private readonly IEmailService _email;

        public BusService(BusDbContext context,
             IMapper mapper,
             IFileService file,
             ILineService line,
             IStopPointService stop,
             ILineStopPointService lineSP,
             INotificationService notification,
             IEmailService email)
        {
            _context = context;
            _mapper = mapper;
            _file = file;
            _line = line;
            _stop = stop;
            _lineSP = lineSP;
            _notification = notification;
            _email = email;
        }

        public async Task<LoginResponse> Login(LoginBusDto dto)
        {
            var bus = await _context.Buses
                        .Include(x => x.Company)
                        .ThenInclude(x => x.City)
                        .FirstOrDefaultAsync(x => !x.IsDelete && x.Number == dto.Number && x.Password == dto.Password);

            if (bus == null)
            {
                throw new InvalidUsernameOrPassword();
            }

            //var busVM = _mapper.Map<BusViewModel>(bus);
            var busVM = _mapper.Map<BusResponse>(bus);
            var result = new LoginResponse
            {
                AccessToken = GenerateAccessToken(bus),
                Bus = busVM
            };

            return result;
        }
        public async Task<bool> Logout(int id)
        {
            var bus = await _context.Buses
                .FirstOrDefaultAsync(x => !x.IsDelete && x.Id == id);

            if (bus == null)
            {
                return false;
            }

            var outOfLine = await _line.LeaveBusInLine(id);


            return outOfLine;
        }
        public async Task<List<BusViewModel>> GetAll(int companyId = 0)
        {
            var query = _context.Buses
                .Where(x => !x.IsDelete)
                .Include(x => x.Company);

            if (companyId != 0)
            {
                query = query.Where(x => x.CompanyId == companyId)
                    .Include(x => x.Company);
            }

            var buses = await query.ToListAsync();


            var busesVM = _mapper.Map<List<BusViewModel>>(buses);
            foreach (var bus in busesVM)
            {
                var line = await _line.GetByBusId(bus.Id);
                bus.IsActive = line != null;
            }

            return busesVM;
        }
        public async Task<BusViewModel> Get(int busId)
        {
            var bus = await _context.Buses
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => !x.IsDelete && x.Id == busId);

            if (bus == null)
            {
                return null;
            }

            var busVM = _mapper.Map<BusViewModel>(bus);

            var line = await _line.GetByBusId(bus.Id);
            busVM.IsActive = line != null;

            busVM.Line = line;
            return busVM;
        }
        public async Task<BusViewModel> GetByLineId(int id)
        {
            var bus = await _context.Buses
                 .Include(x => x.Company)
                 .FirstOrDefaultAsync(x => !x.IsDelete && x.Id == id);

            if (bus == null)
            {
                return null;
            }

            var busVM = _mapper.Map<BusViewModel>(bus);
            return busVM;
        }
        public async Task<bool> ShareLocation(int id, LocationDto dto)
        {
            var bus = await _context.Buses
                .FirstOrDefaultAsync(x => !x.IsDelete && x.Id == id);
            var line = await _line.GetByBusId(id);


            if (bus == null)
            {
                throw new EntityNotFoundException();
            }
            bus.Latitude = dto.Latitude;
            bus.Longitude = dto.Longitude;
            await _context.SaveChangesAsync();

            var LinesSP = line.LinesSP.OrderBy(x => x.Order).ToList();


            foreach (var linesSP in LinesSP)
            {
                if (!linesSP.IsAccess)
                {
                    var dis = CalculateDistance(linesSP.StopPoint.Latitude, linesSP.StopPoint.Longitude,
                                                              dto.Latitude, dto.Longitude);
                    if (dis < 20.0)
                    {
                        var updateDto = new UpdateLineStopPointDto
                        {
                            Id = linesSP.Id,
                            Order = linesSP.Order,
                            TimeAccess = linesSP.TimeAccess,
                        };

                        var result = await _lineSP.Access(updateDto);
                        break;
                    }
                }
            }


            return true;

        }
        public async Task<int> Delete(int id)
        {
            var bus = await _context.Buses
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (bus == null)
            {
                throw new EntityNotFoundException();
            }

            bus.IsDelete = true;
            await _context.SaveChangesAsync();
            return bus.Id;
        }
        public async Task<int> Create(CreateBusDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var bus = _mapper.Map<CreateBusDto, Bus>(dto);
            bus.Latitude = 0.0;
            bus.Longitude = 0.0;

            bus.CreatedBy = "" + dto.CompanyId;

            if (dto.Image != null)
            {
                bus.ImgPath = await _file.SaveFile(dto.Image, "BusImage");
                bus.ImgPath = Paths.ImageBusPath + bus.ImgPath;
            }

            await _context.Buses.AddAsync(bus);
            await _context.SaveChangesAsync();
            return bus.Id;
        }
        public async Task<int> Update(UpdateBusDto dto)
        {
            var bus = await _context.Buses
                .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);

            if (bus == null)
            {
                throw new EntityNotFoundException();
            }

            if (dto.Image != null)
            {
                bus.ImgPath = await _file.SaveFile(dto.Image, "BusImage");
                bus.ImgPath = Paths.ImageBusPath + bus.ImgPath;
            }

            var busUpdate = _mapper.Map(dto, bus);
            busUpdate.UpdatedAt = DateTime.Now;
            busUpdate.UpdatedBy = "" + bus.CompanyId;

            _context.Buses.Update(busUpdate);
            await _context.SaveChangesAsync();
            return bus.Id;
        }
        private string GenerateAccessToken(Bus bus)
        {
            var claims = new List<Claim>()
            {
            new Claim(JwtRegisteredClaimNames.Sub , bus.Number),
            new Claim(JwtRegisteredClaimNames.Name , bus.Name),
            new Claim("CompanyId" , bus.CompanyId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jgghhkiderhvbNMfghjgjgghhkiderhvbNMfghjg"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddHours(10);

            var accessToken = new JwtSecurityToken("http://mohammedjmain-001-site1.atempurl.com/",
                "http://mohammedjmain-001-site1.atempurl.com/",
                claims,
                expires: expires,
                signingCredentials: credentials
                );

            //var result = new AccessTokenViewModel()
            //{
            //    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            //    ExpireAt = expires
            //};

            var result = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return result;
        }
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = ConvertToRadians(lat2 - lat1);
            var dLon = ConvertToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ConvertToRadians(lat1)) * Math.Cos(ConvertToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;
            return distance * 1000; // Convert distance to meters
        }
        private double ConvertToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

    }
}

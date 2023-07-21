using AutoMapper;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.Dtos.StopPointDtos;
using BusTracking.Core.Dtos.UserDtos;
using BusTracking.Core.Responses;
using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.Responses.CompanyResponses;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CityViewModels;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Core.ViewModels.UserViewModels;
using BusTracking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.AutoMappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.ImageUrl, x => x.Ignore());

            CreateMap<Bus, BusResponse>();
            CreateMap<Bus, BusViewModel>();
            CreateMap<CreateBusDto, Bus>().ForMember(x => x.ImgPath, x => x.Ignore());
            CreateMap<UpdateBusDto, Bus>().ForMember(x => x.ImgPath, x => x.Ignore());
            CreateMap<Bus, UpdateBusDto>();

            CreateMap<Company, CompanyResponse>();
            CreateMap<Company, CompanyViewModel>();
            CreateMap<CreateCompanyDto, Company>().ForMember(x => x.Logo, x => x.Ignore());
            CreateMap<UpdateCompanyDto, Company>().ForMember(x => x.Logo, x => x.Ignore());
            CreateMap<Company, UpdateCompanyDto>();

            CreateMap<Line, BusLineResponse>();
            CreateMap<Line, LineResponse>();
            CreateMap<Line, LineViewModel>();
            CreateMap<CreateLineDto, Line>();
            CreateMap<UpdateLineDto, Line>();
            CreateMap<Line, UpdateLineDto>();


            CreateMap<StopPoint, StopPointResponse>().ForMember(x => x.LineSP, x => x.Ignore());
            CreateMap<StopPoint, StopPointViewModel>();
            CreateMap<CreateStopPointDto, StopPoint>();
            CreateMap<UpdateStopPointDto, StopPoint>();
            CreateMap<StopPoint, UpdateStopPointDto>();

            CreateMap<LineStopPoint, LineStopPointViewModel>();
            CreateMap<LineStopPointViewModel, LineStopPoint>();
            CreateMap<CreateLineStopPointDto, LineStopPoint>();
            CreateMap<UpdateLineStopPointDto, LineStopPoint>();
            CreateMap<LineStopPoint, UpdateLineStopPointDto>();

            CreateMap<City, CityResponse>();
            CreateMap<City, CityViewModel>();
            //CreateMap<CreateCityDto, City>();
            //CreateMap<UpdateCityDto, City>();
            //CreateMap<City, UpdateCityDto>();   
              CreateMap<Notification, NotificationViewModel>() ;
        }
    }
}

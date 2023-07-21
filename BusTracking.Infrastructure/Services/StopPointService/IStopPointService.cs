using AutoMapper;
using BusTracking.Core.Dtos.StopPointDtos;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.StopPointService
{
    public interface IStopPointService
    {
        Task<List<StopPointViewModel>> GetAll();
        Task<StopPointViewModel> Get(int id);
        Task<int> Create(CreateStopPointDto dto);
        Task<int> Delete(int id);
        Task<int> Update(UpdateStopPointDto dto);
        Task<List<StopPointResponse>> GetAllAPI();
        Task<StopPointResponse> GetAPI(int id);
    }
}

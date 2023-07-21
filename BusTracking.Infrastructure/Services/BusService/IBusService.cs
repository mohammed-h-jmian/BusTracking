using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.BusViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.BusService
{
    public interface IBusService
    {
        Task<List<BusViewModel>> GetAll(int CompanyId = 0);
        Task<BusViewModel> Get(int busId);
        //Task<BusViewModel> GetByLineId(int id);
        Task<bool> ShareLocation(int id ,LocationDto dto);
        Task<int> Delete(int id);
        Task<int> Create(CreateBusDto dto);
        Task<int> Update(UpdateBusDto dto);
        Task<LoginResponse> Login(LoginBusDto dto);
        Task<bool> Logout(int id);
    }
}
using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.LinesViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.LineService
{
    public interface ILineService
    {
        Task<List<LineViewModel>> GetAll(int? companyId = null);
        Task<LineViewModel> Get(int id);
        Task<LineViewModel> GetByBusId(int id);
        Task<int> Delete(int id);
        Task<int> Create(CreateLineDto dto);
        Task<int> Update(UpdateLineDto dto);
        Task<int> GenerateNewCode(int id);

        //api
        Task<bool> LeaveBusInLine(int busId);
        Task<LineResponse> SetBusInLine(SetBusInLineCodeDto dto);
        Task<List<LineResponse>> GetAllByCompanyId(int companyId);
        Task<LineResponse> SetBusInLine(SetBusInLineIdDto dto);
        Task<List<BusLineResponse>> GetActiveLine();
        Task<List<BusLineResponse>> GetAllActiveLine();
        Task<BusLineResponse> GetAPI(int Id);
    }
}

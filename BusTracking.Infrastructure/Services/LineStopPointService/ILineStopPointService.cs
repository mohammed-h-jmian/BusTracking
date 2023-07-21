using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.LineStopPointService
{
    public interface ILineStopPointService
    {
        Task<LineStopPointCompositeViewModel> GetAllByLineId(int lineId);
        Task<int> Delete(int id);
        Task<int> Create(CreateLineStopPointDto dto);
        Task<LineStopPointViewModel> Get(int id);
        Task<bool> Access(UpdateLineStopPointDto dto);
    }
}

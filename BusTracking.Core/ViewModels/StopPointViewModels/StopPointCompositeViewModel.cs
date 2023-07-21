using BusTracking.Core.Dtos.StopPointDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.StopPointViewModels
{
    public class StopPointCompositeViewModel
    {
        public CreateStopPointDto CreateDto { get; set; }
        public UpdateStopPointDto UpdateDto { get; set; }
        public List<StopPointViewModel> ListStopPoints { get; set; }
        public StopPointViewModel StopPoint { get; set; }
    }
}

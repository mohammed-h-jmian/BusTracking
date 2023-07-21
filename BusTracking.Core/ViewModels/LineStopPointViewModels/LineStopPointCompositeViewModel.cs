using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.LineStopPointViewModels
{
    public class LineStopPointCompositeViewModel
    {
        public LineViewModel Line { get; set; }
        public LineStopPointViewModel LineStopPoint { get; set; }
        public List<LineStopPointViewModel> ListLineStopPoints { get; set; }
        public List<StopPointViewModel> StopPoints { get; set; }
        public CreateLineStopPointDto CreateDto { get; set;}
        public UpdateLineStopPointDto UpdateDto { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;

namespace BusTracking.Core.ViewModels.LineStopPointViewModels
{
    public class LineStopPointViewModel
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public LineViewModel Line { get; set; }
        public int StopPointId { get; set; }
        public StopPointViewModel StopPoint { get; set; }
        public string TimeAccess { get; set; }
        public bool IsAccess { get; set; }
        public int Order { get; set; }
    }
}

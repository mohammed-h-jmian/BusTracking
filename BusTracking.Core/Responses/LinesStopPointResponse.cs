using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class LinesStopPointResponse
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string BusNumber { get; set; }
        public string ImgPath { get; set; }
        public string BusCompany { get; set; }
        public string TimeAccess { get; set; }
        public bool IsAccess { get; set; }
    }
}

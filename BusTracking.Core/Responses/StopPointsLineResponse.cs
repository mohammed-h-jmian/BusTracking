using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class StopPointsLineResponse
    {
        public int StopPointId { get; set; }
        public string StopPointName { get; set; }
        public string TimeAccess { get; set; }
        public bool IsAccess { get; set; }
    }
}

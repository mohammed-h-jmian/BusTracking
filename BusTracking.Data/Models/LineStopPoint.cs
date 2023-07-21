using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class LineStopPoint : BaseEntity
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public Line Line { get; set; }
        public int StopPointId { get; set; }
        public StopPoint StopPoint { get; set; }
        public string TimeAccess { get; set; }
        public bool IsAccess { get; set; }
        public int Order { get; set; }

        public LineStopPoint() { 
        IsAccess = false;
        }
    }
}

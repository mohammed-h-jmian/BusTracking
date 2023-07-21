using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class Line : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Code { get; set; }
        public DateTime ActiveExpired { get; set; }
        public int? BusId { get; set; } = null;
        public Bus Bus { get; set; }
        public List<LineStopPoint> LinesSP { get; set; }
        public Status LineStatus { get; set; }

    }
}
 
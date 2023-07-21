using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class Company : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public List<Line> Lines { get; set; }
        public List<Bus> Buses { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public Status Status { get; set; }

        public Company()
        {
            Status = Status.Pending;
        }

    }
}

using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class Bus : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Password { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ImgPath { get; set; }
        public Status BusStatus { get; set; }

        public Bus()
        {
            IsDelete = false;  
        }
    }
}

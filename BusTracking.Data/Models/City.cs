using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class City : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
    }
}

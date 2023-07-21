using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Data.Models
{
    public class Notification 
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public Notification()
        {
            CreatedAt = DateTime.Now;
            IsRead = false;
        }

    }
}

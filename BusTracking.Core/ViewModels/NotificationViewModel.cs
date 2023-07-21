using BusTracking.Core.ViewModels.CompanyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int? CompanyId { get; set; }
        public CompanyViewModel? Company { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

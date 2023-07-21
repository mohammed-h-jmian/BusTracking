using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels
{
    public class AccessTokenViewModel
    {
        public string AccessToken   { get; set; }
        public DateTime ExpireAt   { get; set; }
    }
}

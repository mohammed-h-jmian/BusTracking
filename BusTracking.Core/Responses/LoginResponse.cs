using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public BusResponse Bus { get; set; }
    }
}

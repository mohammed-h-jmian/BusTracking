using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class APIResponse
    {
        public bool Status { get; set; }
        public object Data { get; set; }

        public APIResponse(bool status, object data)
        {
            Status = status;
            Data = data;
        }
    }
}

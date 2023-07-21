using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Responses
{
    public class Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public Response(bool status, string message )
        {
            Status = status;
            Message = message;
        }
    }
}

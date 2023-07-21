using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Exceptions
{
    public class InvalidUsernameOrPassword : Exception
    {
        public InvalidUsernameOrPassword() : base("Invalid Username Or Password")
        {
        }
    }
}

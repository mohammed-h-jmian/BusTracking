using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Exceptions
{
    public class BusIdAlreadyUsedException : Exception
    {
        public BusIdAlreadyUsedException():base("This bus uses another line") { }
    }
}

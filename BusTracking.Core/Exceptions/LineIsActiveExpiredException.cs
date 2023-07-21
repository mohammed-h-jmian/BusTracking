using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Exceptions
{
    public class LineIsActiveExpiredException : Exception
    {
        public LineIsActiveExpiredException() :base("The line has expired") { }
    }
}

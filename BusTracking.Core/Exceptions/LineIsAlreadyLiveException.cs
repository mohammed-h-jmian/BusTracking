using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Exceptions
{
    public class LineIsAlreadyLiveException : Exception
    {
        public LineIsAlreadyLiveException() : base("The line is already active") { }
    }
}

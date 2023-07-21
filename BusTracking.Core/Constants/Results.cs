using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Constants
{
    public class OperationResults
    {
        public static object AddSuccessResult()
        {
            return new { status = 1, msg = "s: added successfully", close = 1 };
        }

        public static object EditSuccessResult()
        {
            return new { status = 1, msg = "s: updated successfully", close = 1 };
        }

        public static object UpdateStatusSuccessResult()
        {
            return new { status = 1, msg = "s: Status updated successfully", close = 1 };
        }

        public static object DeleteSuccessResult()
        {
            return new { status = 1, msg = "s: deleted successfully", close = 1 };
        }


    }
}

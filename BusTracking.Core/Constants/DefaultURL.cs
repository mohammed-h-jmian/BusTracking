using BusTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Constants
{
    public class DefaultURL
    {
        public const string AdministratorURL = "/Admin/Home/Index";
        public const string CompanyAdminURL = "/Admin/Company/Profile";
        public const string MapAdminURL = "/Admin/StopPoint/Index";

        public static string GetDefaultURL(string type)
        {
            if (type == UserType.Administrator.ToString())
            {
                return AdministratorURL;
            }
            else if (type == UserType.CompanyAdmin.ToString())
            {
                return CompanyAdminURL;
            }
            else if (type == UserType.MapAdmin.ToString())
            {
                return MapAdminURL;
            }
            else
            {
                throw new ArgumentException("Invalid UserType");
            }
        }

    }
}

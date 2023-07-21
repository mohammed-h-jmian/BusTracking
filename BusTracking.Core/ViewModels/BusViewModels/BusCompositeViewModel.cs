using BusTracking.Core.Dtos.BusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.BusViewModels
{
    public class BusCompositeViewModel
    {
        public UpdateBusDto Dto { get; set; }
        public BusViewModel Bus { get; set; }
    }
}

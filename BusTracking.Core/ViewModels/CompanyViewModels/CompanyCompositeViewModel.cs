using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Dtos.LineDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.CompanyViewModels
{
    public class CompanyCompositeViewModel
    {
        public CompanyViewModel Company { get; set; }
        public UpdateCompanyDto UpdateDto { get; set; }
        public CreateLineDto CreateLineDto { get; set; }
        //public CreateBusDto CreateBuseDto { get; set; }
    }
}

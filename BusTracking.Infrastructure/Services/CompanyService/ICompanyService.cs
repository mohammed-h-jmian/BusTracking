using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.ViewModels.CompanyViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<List<CompanyViewModel>> GetAll();
        Task<CompanyViewModel> Get(int id);
        Task<int> Delete(int id);
        Task<int> Active(int id);
        Task<int> Pending(int id);
        Task<int> Update(UpdateCompanyDto dto); 
        Task<int> Create(CreateCompanyDto dto); 
        Task<bool> Remove(int companyId);
    }
}

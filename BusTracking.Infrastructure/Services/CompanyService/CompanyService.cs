
﻿using AutoMapper;
using BusTracking.Core.Constants;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.CompanyDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.Exceptions;

using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.BusService;
using BusTracking.Infrastructure.Services.EmailService;
using BusTracking.Infrastructure.Services.FileService;
using BusTracking.Infrastructure.Services.LineService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BusTracking.Infrastructure.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _file;
        private readonly ILineService _line;
        private readonly IBusService _bus;
        private readonly IEmailService _email;

        public CompanyService(BusDbContext context,
            IMapper mapper,
            IFileService file ,
            ILineService line,
            IBusService bus,
            IEmailService email)
        {
            _context = context;
            _mapper = mapper;
            _file = file;
            _line = line;
            _bus = bus;
            _email = email;
        }

        public async Task<List<CompanyViewModel>> GetAll()
        {
            var companies = await _context.Companies
                .Include(x => x.Buses.Where(b => !b.IsDelete))
                .Include(x => x.Lines.Where(b => !b.IsDelete))
                .Where(x => !x.IsDelete)
                .ToListAsync();


            var companiesVM = _mapper.Map<List<CompanyViewModel>>(companies);
            return companiesVM;
        }

        public async Task<CompanyViewModel> Get(int id)
        {
            var company = await _context.Companies
                .Include(x => x.Buses.Where(b => !b.IsDelete))
                .Include(x => x.Lines.Where(b => !b.IsDelete))
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (company == null)
            {
            return null;

            }

            var companyVM = _mapper.Map<CompanyViewModel>(company);
            foreach (var bus in companyVM.Buses)
            {
                var line = await _line.GetByBusId(bus.Id);
                bus.IsActive = line != null;
            }
            
            
            return companyVM;
        }

        public async Task<int> Delete(int id)
        {

            var company = await _context.Companies
                .Include(x=>x.Buses)
                .Include(x=>x.Lines)
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (company == null)
            {
                throw new EntityNotFoundException();
            }

            company.IsDelete = true;

            foreach (var bus in company.Buses) {
                await _bus.Delete(bus.Id);
            }
            foreach (var line in company.Lines)
            {
                await _line.Delete(line.Id);
            }

            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<int> Active(int id)
        {

            var company = await _context.Companies
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (company == null)
            {
                throw new EntityNotFoundException();
            }

            company.Status = Status.Activated;

            await _context.SaveChangesAsync();

            await _email.Send("mmoh0592069839@gmail.com", "ewrtert", "rtertertert ert ertert ert erwt ewrt ");
            return company.Id;
        }

        public async Task<int> Pending(int id)
        {

            var company = await _context.Companies
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (company == null)
            {
                throw new EntityNotFoundException();
            }

            company.Status = Status.Pending;

            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<int> Update(UpdateCompanyDto dto)
        {

            var company = await _context.Companies
                .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);

            if (company == null)
            {
                throw new EntityNotFoundException();
            }

            if (dto.Logo != null)
            {
                company.Logo = await _file.SaveFile(dto.Logo, "Logo");
                company.Logo = Paths.baseUrl + Paths.LogoCompanyPath + company.Logo;
            }

            _mapper.Map(dto, company);
            await _context.SaveChangesAsync();
            return company.Id;
        }
        public async Task<int> Create(CreateCompanyDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var company = _mapper.Map<CreateCompanyDto, Company>(dto);
            company.CreatedBy = "df"; // Consider removing this line or make it dynamic.

            await _context.Companies.AddAsync(company);

            if (dto.Logo != null)
            {
                company.Logo = await _file.SaveFile(dto.Logo, "Logo");
                company.Logo = Paths.baseUrl + Paths.ImageBusPath + company.Logo;
            }

            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<bool> Remove(int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);

            if (company == null)
            {
                return false;
            }

            // Remove the company and save changes
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

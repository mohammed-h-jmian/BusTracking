using AutoMapper;
using BusTracking.Core.Enums;
using BusTracking.Core.Responses;
using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.ViewModels;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.CompanyViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;

        public DashboardService(BusDbContext context
            , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DashboardViewModel> GetData()
        {
            var data = new DashboardViewModel();
            data.NumberOfBus = await _context.Buses.CountAsync(x => !x.IsDelete);
            data.NumberOfCompany = await _context.Companies.CountAsync(x => !x.IsDelete);
            data.NumberOfStopPoint = await _context.StopPoints.CountAsync(x => !x.IsDelete);
            data.NumberOfLine = await _context.Lines.CountAsync(x => !x.IsDelete);

            var buses = await _context.Buses
                .Include(x => x.Company)
                .Where(x => !x.IsDelete)
                .Take(5)
                .ToListAsync();
            var companies = await _context.Companies
                .Include(x => x.City)
                .Where(x => !x.IsDelete)
                .Take(5)
                .ToListAsync();
            var stopPoints = await _context.StopPoints
                .Include(x => x.City)
                .Where(x => !x.IsDelete)
                .Take(5)
                .ToListAsync();
            var lines = await _context.Lines
                .Include(x => x.Company)
                .Where(x => !x.IsDelete)
            .Take(5)
                .ToListAsync();

            var busesVM = _mapper.Map<List<BusViewModel>>(buses);
            var companiesVM = _mapper.Map<List<CompanyViewModel>>(companies);
            var stopPointsVM = _mapper.Map<List<StopPointViewModel>>(stopPoints);
            var linesVM = _mapper.Map<List<LineViewModel>>(lines);


            data.Buses = busesVM;
            data.Companies = companiesVM;
            data.StopPoints = stopPointsVM;
            data.Lines = linesVM;

            return data;
        }
        public async Task<List<BusResponse>> GetActiveBus()
        {
            var buses = await _context.Buses
                .Include(x => x.Company)
                .Where(x => !x.IsDelete && x.BusStatus == Status.Activated)
                .Take(5)
                .ToListAsync();

            var busesVM = _mapper.Map<List<BusResponse>>(buses);

            return busesVM;
        }




    }
}

using AutoMapper;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Data.Models;
using BusTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTracking.Core.Dtos.LineDtos;
using Microsoft.EntityFrameworkCore;
using BusTracking.Core.ViewModels.CityViewModels;

namespace BusTracking.Infrastructure.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;

        public CityService(BusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CityViewModel>> GetAll()
        {

            var cities = await _context.Cities
                .Include(x => x.Companies.Where(d=>!d.IsDelete))
                .ToListAsync();

            var citiesVM = _mapper.Map<List<City>, List<CityViewModel>>(cities);

            return citiesVM;
        }
        //public async Task<int> Create(CreateLineDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ArgumentNullException(nameof(dto));
        //    }

        //    var line = _mapper.Map<CreateLineDto, Line>(dto);
        //    line.CompanyId = 2;

        //    string randomSymbol = GenerateRandomCode();

        //    while (await _context.Lines.AnyAsync(l => l.Code == randomSymbol))
        //    {
        //        randomSymbol = GenerateRandomCode();
        //    }

        //    line.Code = randomSymbol;
        //    line.CreatedBy = "df";

        //    await _context.Lines.AddAsync(line);
        //    await _context.SaveChangesAsync();

        //    return line.Id;
        //}
    }
}

using AutoMapper;
using BusTracking.Core.Dtos.StopPointDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.Exceptions;
using BusTracking.Core.Responses;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.StopPointService
{
    public class StopPointService : IStopPointService
    {

        private readonly BusDbContext _context;
        private readonly IMapper _mapper;

        public StopPointService(BusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StopPointViewModel>> GetAll()
        {
            var stopPoints = await _context.StopPoints
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                .ThenInclude(x => x.Line)
                .Include(x => x.City)
                .Where(x => !x.IsDelete)
                .ToListAsync();
            var stopPointsVM = _mapper.Map<List<StopPoint>, List<StopPointViewModel>>(stopPoints);
            return stopPointsVM;
        }
        public async Task<StopPointViewModel> Get(int id)
        {
            var stopPoint = await _context.StopPoints
                .Include(x => x.City)
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                    .ThenInclude(linesSP => linesSP.Line)
                        .ThenInclude(line => line.Company)
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                    .ThenInclude(linesSP => linesSP.Line)
                        .ThenInclude(line => line.Bus)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);


            if (stopPoint == null)
            {
                return null;
            }

            var stopPointVM = _mapper.Map<StopPoint, StopPointViewModel>(stopPoint);
            return stopPointVM;
        }

        public async Task<List<StopPointResponse>> GetAllAPI()
        {
            var stopPoints = await _context.StopPoints
                .Include(x => x.City)
                .Where(x => !x.IsDelete)
                .ToListAsync();

            var stopPointsVM = _mapper.Map<List<StopPoint>, List<StopPointResponse>>(stopPoints);
            return stopPointsVM;
        }

        public async Task<StopPointResponse> GetAPI(int id)
        {

            var stopPoint = await _context.StopPoints
                .Where(x => !x.IsDelete)
                .Include(x => x.City)
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                    .ThenInclude(linesSP => linesSP.Line)
                        .ThenInclude(line => line.Company)
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                    .ThenInclude(linesSP => linesSP.Line)
                        .ThenInclude(line => line.Bus)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);


            if (stopPoint == null)
            {
                return null;
            }

            var stopPointVM = _mapper.Map<StopPoint, StopPointResponse>(stopPoint);


            foreach (var lineSP in stopPoint.LinesSP)
            {
                if (lineSP.Line != null)
                {
                    if (lineSP.Line.LineStatus == Status.Activated && !lineSP.Line.IsDelete && lineSP.Line.Bus !=null)
                    {
                        var newLineSP = new LinesStopPointResponse
                        {
                            Id = lineSP.Id,
                            BusId = (int)lineSP.Line.BusId,
                            BusNumber = lineSP.Line.Bus.Number,
                            ImgPath = lineSP.Line.Bus.ImgPath,
                            BusCompany = lineSP.Line.Bus.Company.Name,
                            IsAccess = lineSP.IsAccess,
                            TimeAccess = lineSP.TimeAccess
                        };
                        stopPointVM.LineSP.Add(newLineSP);
                    }
                }


            }

            return stopPointVM;
        }

        public async Task<int> Create(CreateStopPointDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var stopPoint = _mapper.Map<CreateStopPointDto, StopPoint>(dto);
            stopPoint.CreatedBy = "df"; // delete
            await _context.StopPoints.AddAsync(stopPoint);
            await _context.SaveChangesAsync();
            return stopPoint.Id;
        }
        public async Task<int> Delete(int id)
        {
            var point = await _context.StopPoints.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (point == null)
            {
                throw new EntityNotFoundException();
            }
            point.IsDelete = true;
            _context.StopPoints.Update(point);
            await _context.SaveChangesAsync();
            return point.Id;
        }

        public async Task<int> Update(UpdateStopPointDto dto)
        {
            var point = await _context.StopPoints.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (point == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedPoint = _mapper.Map<UpdateStopPointDto, StopPoint>(dto, point);
            _context.StopPoints.Update(updatedPoint);
            await _context.SaveChangesAsync();
            return updatedPoint.Id;
        }

    }
}

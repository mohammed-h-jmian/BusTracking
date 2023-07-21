using AutoMapper;
using BusTracking.Core.Constants;
using BusTracking.Core.Dtos.BusDtos;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.Dtos.LineStopPointDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.Exceptions;
using BusTracking.Core.ViewModels.BusViewModels;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Core.ViewModels.StopPointViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.FileService;
using BusTracking.Infrastructure.Services.LineService;
using BusTracking.Infrastructure.Services.StopPointService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.LineStopPointService
{
    public class LineStopPointService : ILineStopPointService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStopPointService _stopPoint;
        private readonly ILineService _line;

        public LineStopPointService(BusDbContext context,
           IMapper mapper, IStopPointService stopPoint, ILineService line)
        {
            _context = context;
            _mapper = mapper;
            _stopPoint = stopPoint;
            _line = line;
        }

        public async Task<LineStopPointCompositeViewModel> GetAllByLineId(int lineId)
        {
            var line = await _line.Get(lineId);
            var lineVM = _mapper.Map<LineViewModel>(line);

            var stopPoint = await _stopPoint.GetAll();
            var stopPointVM = _mapper.Map<List<StopPointViewModel>>(stopPoint);

            var compositeVM = new LineStopPointCompositeViewModel
            {
                StopPoints = stopPointVM,
                Line = lineVM
            };
            return compositeVM;
        }
        public async Task<int> Create(CreateLineStopPointDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var lineStopPoint = _mapper.Map<CreateLineStopPointDto, LineStopPoint>(dto);
            lineStopPoint.CreatedBy = "df"; // Consider removing this line or make it dynamic.

            await _context.LineStopPoints.AddAsync(lineStopPoint);
            await _context.SaveChangesAsync();
            return lineStopPoint.Id;
        }

        public async Task<int> Delete(int id)
        {
            var lineStopPoint = await _context.LineStopPoints
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (lineStopPoint == null)
            {
                throw new EntityNotFoundException();
            }

            lineStopPoint.IsDelete = true;
            await _context.SaveChangesAsync();
            return lineStopPoint.Id;
        }
        public async Task<LineStopPointViewModel> Get(int id)
        {
            var lineStopPoint = await _context.LineStopPoints
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (lineStopPoint == null)
            {
                return null;
            }

            var lineStopPointVM = _mapper.Map<LineStopPointViewModel>(lineStopPoint);


            return lineStopPointVM;
        }
        public async Task<bool> Access(UpdateLineStopPointDto dto)
        {
            var lineSP = await _context.LineStopPoints.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (lineSP == null)
            {
                return false;
            }
 
            var updatedLineSP = _mapper.Map<UpdateLineStopPointDto, LineStopPoint>(dto, lineSP);
            updatedLineSP.IsAccess = true;
            _context.LineStopPoints.Update(updatedLineSP);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

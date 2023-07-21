using AutoMapper;
using BusTracking.Core.Dtos.APIDtos;
using BusTracking.Core.Dtos.LineDtos;
using BusTracking.Core.Enums;
using BusTracking.Core.Exceptions;
using BusTracking.Core.Responses;
using BusTracking.Core.Responses.BusResponses;
using BusTracking.Core.ViewModels.LineStopPointViewModels;
using BusTracking.Core.ViewModels.LinesViewModels;
using BusTracking.Data;
using BusTracking.Data.Models;
using BusTracking.Infrastructure.Services.EmailService;
using BusTracking.Infrastructure.Services.NotificationService;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BusTracking.Infrastructure.Services.LineService
{
    public class LineService : ILineService
    {
        private readonly BusDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notification;
        private readonly IEmailService _email;

        public LineService(BusDbContext context,
            IMapper mapper,
            INotificationService notification,
            IEmailService email)
        {
            _context = context;
            _mapper = mapper;
            _notification = notification;
            _email = email;
        }
        public async Task<List<LineResponse>> GetAllByCompanyId(int companyId)
        {
            var lines = await _context.Lines
                .Include(x => x.Company)
                .ThenInclude(x => x.City)
                .Where(x => !x.IsDelete && x.ActiveExpired > DateTime.Now && x.CompanyId == companyId && x.LineStatus == Status.Activated).ToListAsync();

            if (!lines.Any())
            {
                return null;
            }


            var linesVM = _mapper.Map<List<LineResponse>>(lines);

            return linesVM;
        }
        public async Task<List<LineViewModel>> GetAll(int? companyId = 0)
        {
            var query = _context.Lines.Where(x => !x.IsDelete);

            if (companyId != 0)
            {
                query = query.Where(x => x.CompanyId == companyId.Value);
            }

            var lines = await query
                .Include(x => x.Company)
                .Include(x => x.Bus)
                .ToListAsync();

            var linesVM = _mapper.Map<List<LineViewModel>>(lines);

            return linesVM;
        }
        public async Task<List<BusLineResponse>> GetActiveLine()
        {
            var lines = await _context.Lines
                .Where(x => !x.IsDelete && x.LineStatus == Status.Activated && x.ActiveExpired > DateTime.Now && (x.BusId != 0 && x.BusId != null))
                .Include(x => x.Bus)
                .ThenInclude(x => x.Company)
                .Take(5)
                .ToListAsync();

            //var linesVM = _mapper.Map<List<BusLineResponse>>(lines);
            var linesVM = lines.Select(line => new BusLineResponse
            {
                Id = line.Id,
                Name = line.Name,
                BusId = (int)line.BusId,
                Bus = _mapper.Map<BusResponse>(line.Bus)
            }).ToList();


            return linesVM;
        }
        public async Task<List<BusLineResponse>> GetAllActiveLine()
        {
            var lines = await _context.Lines
                .Where(x => !x.IsDelete && x.LineStatus == Status.Activated && x.ActiveExpired > DateTime.Now && (x.BusId != 0 && x.BusId != null))
                .Include(x => x.Bus)
                .ThenInclude(x => x.Company)
                .ToListAsync();

            //var linesVM = _mapper.Map<List<BusLineResponse>>(lines);
            var linesVM = lines.Select(line => new BusLineResponse
            {
                Id = line.Id,
                Name = line.Name,
                BusId = (int)line.BusId,
                Bus = _mapper.Map<BusResponse>(line.Bus)
            }).ToList();


            return linesVM;
        }
        public async Task<BusLineResponse> GetAPI(int Id)
        {
            var line = await _context.Lines
                .Include(x => x.LinesSP.Where(x => !x.IsDelete))
               .ThenInclude(x => x.StopPoint)
                .Include(x => x.Bus)
                .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync(x => !x.IsDelete && x.LineStatus == Status.Activated && x.ActiveExpired > DateTime.Now && x.Id == Id && (x.BusId != 0 && x.BusId != null));

            if (line == null)
            {
                return null;
            }

            var lineVM = _mapper.Map<BusLineResponse>(line);

            foreach (var lineSP in line.LinesSP)
            {
                if (lineSP.StopPoint != null)
                {
                    if (lineSP.Line.LineStatus == Status.Activated && !lineSP.Line.IsDelete && lineSP.Line.Bus != null)
                    {
                        var newLineSP = new StopPointsLineResponse
                        {
                            StopPointId = lineSP.StopPoint.Id,
                            StopPointName = lineSP.StopPoint.Name,
                            IsAccess = lineSP.IsAccess,
                            TimeAccess = lineSP.TimeAccess
                        };
                        lineVM.LineSP.Add(newLineSP);
                    }
                }


            }

            return lineVM;


        }
        public async Task<LineViewModel> GetByBusId(int id)
        {
            var line = await _context.Lines
                 .Include(x => x.Bus)
                 .Include(x => x.Company)
                 .Include(x => x.LinesSP.Where(b => !b.IsDelete))
                 .ThenInclude(x => x.StopPoint)
                 .FirstOrDefaultAsync(x => x.BusId == id && !x.IsDelete);

            if (line == null)
            {
                return null;
            }

            var lineVM = _mapper.Map<LineViewModel>(line);
            return lineVM;
        }
        public async Task<LineViewModel> Get(int id)
        {
            var line = await _context.Lines
                 .Include(x => x.Bus)
                 .Include(x => x.Company)
                 .Include(x => x.LinesSP.Where(x => !x.IsDelete))
                    .ThenInclude(x => x.StopPoint)
                        .ThenInclude(x => x.City)
                 .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (line == null)
            {
                return null;
            }

            var lineVM = _mapper.Map<LineViewModel>(line);
            var LineSPoints = await _context.LineStopPoints
            .Include(x => x.StopPoint)
            .Where(x => x.LineId == id && !x.IsDelete)
            .OrderBy(x => x.Order)
            .ToListAsync();

            var LineSPointsVM = _mapper.Map<List<LineStopPointViewModel>>(LineSPoints);

            lineVM.LinesSP = LineSPointsVM;
            return lineVM;
        }
        public async Task<LineResponse> SetBusInLine(SetBusInLineIdDto dto)
        {
            var line = await _context.Lines
                 .Include(x => x.Company)
                 .ThenInclude(x => x.City)
                 .FirstOrDefaultAsync(x => x.Id == dto.LineId && !x.IsDelete);

            if (line == null)
            {
                throw new EntityNotFoundException();
            }
            else if (line.ActiveExpired < DateTime.Now)
            {
                throw new LineIsActiveExpiredException();
            }
            else if (line.BusId != 0 && line.BusId != null)
            {
                throw new LineIsAlreadyLiveException();
            }
            else if (await _context.Lines.AnyAsync(x => x.BusId == dto.BusId && !x.IsDelete))
            {
                throw new BusIdAlreadyUsedException();
            }


            line.BusId = dto.BusId;
            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
          
            var bus = await _context.Buses
      .Include(x => x.Company)
      .ThenInclude(x => x.City)
      .FirstOrDefaultAsync(x => x.Id == dto.BusId && !x.IsDelete);

            await _notification.Create(bus.CompanyId, "Login Bus In app", "The bus number " + bus.Number + " logged into the application at the time: " + DateTime.Now);
            var user = _context.Users
              .SingleOrDefault(x => x.CompanyId == bus.CompanyId && !x.IsDelete);
            try
            {
                await _email.Send(user.Email, "Login Bus In app", "The bus number " + bus.Number + " logged into the application at the time: " + DateTime.Now);

            }
            catch (Exception)
            {


            }
            var lineVM = _mapper.Map<LineResponse>(line);
            return lineVM;
        }
        public async Task<LineResponse> SetBusInLine(SetBusInLineCodeDto dto)
        {
            var line = await _context.Lines
                 .Include(x => x.Company)
                 .ThenInclude(x => x.City)
                 .FirstOrDefaultAsync(x => x.Code == dto.LineCode && !x.IsDelete);

            if (line == null)
            {
                throw new EntityNotFoundException();
            }
            else if (line.ActiveExpired < DateTime.Now)
            {
                throw new LineIsActiveExpiredException();
            }
            else if (line.BusId != 0 && line.BusId != null)
            {
                throw new LineIsAlreadyLiveException();
            }
            else if (await _context.Lines.AnyAsync(x => x.BusId == dto.BusId && !x.IsDelete))
            {
                throw new BusIdAlreadyUsedException();
            }


            line.BusId = dto.BusId;



            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
            var lineVM = _mapper.Map<LineResponse>(line);
            return lineVM;
        }
        public async Task<bool> LeaveBusInLine(int busId)
        {
            var line = await _context.Lines
                 .FirstOrDefaultAsync(x => !x.IsDelete && x.BusId == busId);

            if (line == null)
            {
                return false;
                //throw new EntityNotFoundException();
            }

            line.BusId = null;

            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
            var lineVM = _mapper.Map<LineResponse>(line);
            return true;
        }

        public async Task<int> Delete(int id)
        {
            var line = await _context.Lines.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (line == null)
            {
                throw new EntityNotFoundException();
            }
            line.IsDelete = true;
            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
            return line.Id;
        }

        public async Task<int> Create(CreateLineDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var line = _mapper.Map<CreateLineDto, Line>(dto);

            string randomSymbol = GenerateRandomCode();

            while (await _context.Lines.AnyAsync(l => l.Code == randomSymbol))
            {
                randomSymbol = GenerateRandomCode();
            }

            line.Code = randomSymbol;
            line.CreatedBy = "df";
            line.ActiveExpired = DateTime.Now.AddMonths(1);

            await _context.Lines.AddAsync(line);
            await _context.SaveChangesAsync();

            return line.Id;
        }

        public async Task<int> Update(UpdateLineDto dto)
        {
            var line = await _context.Lines.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (line == null)
            {
                throw new EntityNotFoundException();
            }

            var updatedLine = _mapper.Map<UpdateLineDto, Line>(dto, line);
            _context.Lines.Update(updatedLine);
            await _context.SaveChangesAsync();
            return updatedLine.Id;
        }

        public async Task<int> GenerateNewCode(int id)
        {
            var line = await _context.Lines.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);
            if (line == null)
            {
                throw new EntityNotFoundException();
            }
            string randomSymbol = GenerateRandomCode();

            while (await _context.Lines.AnyAsync(l => l.Code == randomSymbol))
            {
                randomSymbol = GenerateRandomCode();
            }
            line.Code = randomSymbol;

            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
            return line.Id;
        }
        private string GenerateRandomCode()
        {
            string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            // Generate a random symbol of length 10
            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(symbols.Length);
                sb.Append(symbols[index]);
            }

            return sb.ToString();
        }

    }
}

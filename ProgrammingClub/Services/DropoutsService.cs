using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public class DropoutsService : IDropoutsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;
        public DropoutsService(
            ProgrammingClubDataContext context,
            IEventsService eventsService,
            IMapper mapper)
        {
            _context = context;
            _eventsService = eventsService;
            _mapper = mapper;
        }
        public async Task CreateDropout(CreateDropout dropout)
        {
            if (!await _eventsService.EventExistByIdAsync(dropout.IDEvent))
            {
                throw new Exception(ErrorMessagesEnum.ID_NotFound);
            }
            if (await GetDropoutByEventID(dropout.IDEvent) != null)
            {
                throw new Exception(ErrorMessagesEnum.AlreadyExistsById);
            }
            var newDropout = _mapper.Map<Dropout>(dropout);
            newDropout.IDDropout = Guid.NewGuid();
            _context.Entry(newDropout).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteDropout(Guid id)
        {
            if (!await DropoutExistByIdAsync(id))
                throw new Exception(ErrorMessagesEnum.ID_NotFound);

            _context.Dropouts.Remove(new Dropout { IDDropout = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dropout?> GetDropoutById(Guid id)
        {
            if (!await DropoutExistByIdAsync(id))
            {
                throw new Exception(ErrorMessagesEnum.Dropout_ID_NotFound);
            }
            return await _context.Dropouts.FirstOrDefaultAsync(m => m.IDDropout == id);
        }

        public async Task<IEnumerable<Dropout>> GetDropouts()
        {
            return await _context.Dropouts.ToListAsync();
        }

        public async Task<Dropout?> UpdateDropout(Guid idDropout, Dropout dropout)
        {
            await ValidateDropout(idDropout, dropout);
            dropout.IDDropout = idDropout;
            _context.Update(dropout);
            await _context.SaveChangesAsync();
            return dropout;
        }
        private async Task ValidateDropout(Guid idDropout, Dropout dropout)
        {
            if (!await DropoutExistByIdAsync(idDropout))
            {
                throw new Exception(ErrorMessagesEnum.Dropout_ID_NotFound);
            }
            if (dropout.DropoutRate == null)
            {
                throw new Exception(ErrorMessagesEnum.EmptyField);
            }
            if (!await _eventsService.EventExistByIdAsync(dropout.IDEvent))
            {
                throw new Exception(ErrorMessagesEnum.Event_ID_NotFound);
            }
        }
        public async Task<Dropout?> UpdatePartiallyDropout(Guid idDropout, Dropout dropout)
        {
            var dropoutFromDatabase = await GetDropoutById(idDropout);
            if (dropoutFromDatabase == null)
            {
                return null;
            }
            bool needUpdate = false;

            if (dropout.DropoutRate != null && dropoutFromDatabase.DropoutRate != dropout.DropoutRate)
            {
                dropoutFromDatabase.DropoutRate = dropout.DropoutRate;
                needUpdate = true;

            }

            if (dropout.IDEvent.HasValue && dropoutFromDatabase.IDEvent != dropout.IDEvent)
            {
                if (GetDropoutByEventID(dropout.IDEvent) != null)
                {
                    throw new Exception(ErrorMessagesEnum.Event_ID_NotFound);
                }
                dropoutFromDatabase.IDEvent = dropout.IDEvent;
                needUpdate = true;
            }
            if (needUpdate)
            {
                await ValidateDropout(idDropout, dropoutFromDatabase);
                _context.Update(dropoutFromDatabase);
                await _context.SaveChangesAsync();
            }
            return dropoutFromDatabase;
        }
        public async Task<Dropout?> GetDropoutByEventID(Guid? eventID)
        {
            if (eventID == null)
                return null;
            return await _context.Dropouts.FirstOrDefaultAsync(m => m.IDEvent == eventID);
        }
        public async Task<bool> DropoutExistByIdAsync(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessagesEnum.Dropout_ID_NotFound);
            }
            return await _context.Dropouts.CountAsync(dropout => dropout.IDDropout == id) > 0;
        }
    }
}

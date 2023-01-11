using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Exceptions;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using System.Linq;

namespace ProgrammingClub.Services
{
    public class EventParticipantsService:IEventParticipantsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;

        public EventParticipantsService(
            ProgrammingClubDataContext context,
            IMembersService membersService,
            IMapper mapper)
        {
            _context = context;
            _membersService = membersService;
            _mapper = mapper;
        }

        public async Task<bool> GetAllMemberParticipations(Guid? idMember, Guid? idEvent)
        {
            return await _context.EventsParticipants.Where(e => e.IdMember == idMember && e.IdEvent == idEvent).AnyAsync();
        }


        public async Task CreateEventParticipant(CreateEventsParticipant participant)
        {
            if (!await _membersService.MemberExistByIdAsync(participant.IdMember))
                throw new ElementNotFoundInDBException("Member does not exist");

            
            var participants = await GetAllMemberParticipations(participant.IdMember, participant.IdEvent);
            if (participants == true)
            {
                throw new ElementAlreadyExistsInDB("Already in the participant list");
            }

            var newEventParticipant = _mapper.Map<EventsParticipant>(participant);
            newEventParticipant.IdEventParticipant = Guid.NewGuid();

            newEventParticipant.Present = false;
            _context.Entry(newEventParticipant).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEventParticipant(Guid idEventParticipant)
        {
            if (!await EventParticipantExists(idEventParticipant))
                return false;
            
            _context.EventsParticipants.Remove(new EventsParticipant { IdEventParticipant = idEventParticipant });
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<EventsParticipant>> GetEventsParticipantsAsync()
        {
            return await _context.EventsParticipants.ToListAsync();
        }

        public async Task<IEnumerable<EventsParticipant>> GetEventsParticipantsByEventAndPaidAsync(Guid eventId, bool isPaid)
        {
            return await _context.EventsParticipants.Where( x => x.IdEvent == eventId && x.Paid == isPaid).ToListAsync();
        }

        public async Task<IEnumerable<EventsParticipant>> GetEventsParticipantsByEventAndPresentAsync(Guid eventId, bool isPresent)
        {
            return await _context.EventsParticipants.Where( x => x.IdEvent == eventId && x.Present == isPresent).ToListAsync();
        }

        public async Task<IEnumerable<EventsParticipant>> GetAllParticipantsToEvent(Guid eventId)
        {
            return await _context.EventsParticipants.Where( x => x.IdEvent==eventId).ToListAsync();
        }

        public async Task<EventsParticipant?> UpdateEventParticipant(Guid idEventParticipant, CreateEventsParticipant eventParticipant)
        {
            
            if (!await EventParticipantExists(idEventParticipant))
            {
                return null;
            }

            var participantFromDB = await GetEventParticipantById(idEventParticipant);

            if (participantFromDB != null)
            {
                if(participantFromDB.IdMember != eventParticipant.IdMember)
                {
                    var participants = await GetAllMemberParticipations(eventParticipant.IdMember, eventParticipant.IdEvent);
                    if (participants == true)
                    {
                        throw new ElementAlreadyExistsInDB("Already in the participant list for the given event");
                    }
                }

                _context.Entry(participantFromDB).State = EntityState.Detached;
            }

            eventParticipant.IdEventParticipant = idEventParticipant;

            var eventParticipantToSave = _mapper.Map<EventsParticipant>(eventParticipant);

            _context.Update(eventParticipantToSave);
            await _context.SaveChangesAsync();
            return eventParticipantToSave;
        }

        public async Task<EventsParticipant?> UpdateEventParticipantPartially(Guid idEventParticipant, EventsParticipant participant)
        {
            var eventParticipantFromDb = await GetEventParticipantById(idEventParticipant);
            if (eventParticipantFromDb == null)
            {
                return null;
            }

            if (participant.IdEvent != null)
            {
                eventParticipantFromDb.IdEvent = participant.IdEvent; 
            }

            if (participant.IdMember != null)
            {
                
                if(await GetAllMemberParticipations(participant.IdMember, participant.IdEvent) == true)
                {
                    return null;
                }    
                eventParticipantFromDb.IdMember = participant.IdMember;
            }

            if(participant.Paid != null)
            {
                eventParticipantFromDb.Paid = participant.Paid;
            }

            if(participant.Present != null)
            {
                eventParticipantFromDb.Present = participant.Present;
            }


            _context.Update(eventParticipantFromDb);
            await _context.SaveChangesAsync();
            return eventParticipantFromDb;
        }


        public async Task<EventsParticipant?> GetEventParticipantById(Guid id)
        {
            return await _context.EventsParticipants.FirstOrDefaultAsync(e => e.IdEventParticipant == id);
        }

        public async Task<bool> EventParticipantExists(Guid id)
        {
            return await _context.EventsParticipants.CountAsync() > 0;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public class EventsService : IEventsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMapper _mapper;

        public EventsService(ProgrammingClubDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEvent(CreateEvent events)
        {
            var newEvent = _mapper.Map<Event>(events);
            newEvent.IdEvent = Guid.NewGuid();
            _context.Entry(newEvent).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            if(!await EventExistByIdAsync(id)) 
                return false;

            _context.Events.Remove(new Event { IdEvent = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return _context.Events.ToList();
        }

        public async Task<Event?> UpdatePartiallyEvent (Guid id, Event events)
        {
            var eventFromDatabase = await GetEventById(id);
            if (eventFromDatabase == null)
            {
                return null;
            }

            if(events.Name != null)
            {
                eventFromDatabase.Name = events.Name;
            }
            if (events.Description != null)
            {
                eventFromDatabase.Description = events.Description;
            }

            _context.Update(eventFromDatabase);
            await _context.SaveChangesAsync();
            return eventFromDatabase;

        }

        public async Task<Event?> UpdateEvent(Guid id, Event events)
        {
            if(!await EventExistByIdAsync(id)) 
                return null;

            events.IdEvent = id;
            _context.Update(events);
            await _context.SaveChangesAsync();
            return events;
        }

        public async Task<Event?> GetEventById(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.IdEvent == id);
        }

        public async Task<bool> EventExistByIdAsync(Guid? id)
        {
            return await _context.Events.CountAsync(e => e.IdEvent == id) > 0;
        }
    }
}

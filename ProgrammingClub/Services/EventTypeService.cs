using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class EventTypeService : IEventTypeService
       
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IPricingModelsService _pricingModelsService;

        public EventTypeService(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public async Task CreateEventTypeAsync(EventType eventType)
        {
            if (eventType == null)
                throw new Exception();

            eventType.IdEventType = Guid.NewGuid();


            _context.Entry(eventType).State = EntityState.Added;
            await _context.SaveChangesAsync();


        }

        public async Task<bool> DeleteEventTypeAsync(Guid id)
        {
            EventType? eventType = await GetEventTypeByIdAsync(id);
            if (eventType == null)
                return false;
            _context.EventType.Remove(eventType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DbSet<EventType>> GetEventTypesAsync()
        {
            return _context.EventType;
        }

        public async Task<EventType?> GetEventTypeByIdAsync(Guid id)
        {
            return await _context.EventType.FirstOrDefaultAsync (e => e.IdEventType == id);
        }

        public async Task<EventType?> UpdateEventTypeAsync(Guid id, EventType eventType)
        {
            if (GetEventTypeByIdAsync(id) == null)
                return null;
            _context.Update(eventType);
            await _context.SaveChangesAsync();
            return eventType;
        }

        public async Task<EventType?> UpdateEventTypePartiallyAsync(Guid id, EventType eventType)
        {

            var eventTypeFromDatabase = await GetEventTypeByIdAsync(id);

            if (eventTypeFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(eventType.Name))
            {
                eventTypeFromDatabase.Name = eventType.Name;
            }
            if (eventType.Description != null)
            {
                eventTypeFromDatabase.Description = eventType.Description;
            }
            
            if (eventType.IdPricingModel != null)
            {
                eventTypeFromDatabase.IdPricingModel = eventType.IdPricingModel;
            }

            _context.Update(eventType);
            await _context.SaveChangesAsync();
            return eventTypeFromDatabase;
        }

    }
}

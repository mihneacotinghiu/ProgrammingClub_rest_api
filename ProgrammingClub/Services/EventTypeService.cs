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

        public async Task UpdateEventTypeAsync(EventType eventType)
        {
            _context.Update(eventType);
            await _context.SaveChangesAsync();
        }


}
}

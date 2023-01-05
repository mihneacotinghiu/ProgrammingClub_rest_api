using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IEventTypeService
    {

        public Task<DbSet<EventType>> GetEventTypesAsync();
        public Task CreateEventTypeAsync(EventType eventType);
        public Task<EventType?> UpdateEventTypeAsync(Guid id , EventType eventType );
        public Task<bool> DeleteEventTypeAsync(Guid id);
        public Task<EventType?> GetEventTypeByIdAsync(Guid id);
        public Task<EventType?> UpdateEventTypePartiallyAsync (Guid id , EventType eventType);
     
    }
}

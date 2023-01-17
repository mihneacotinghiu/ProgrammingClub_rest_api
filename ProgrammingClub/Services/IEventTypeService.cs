using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IEventTypeService
    {
        public Task<IEnumerable<EventType>> GetEventTypesAsync();
        public Task CreateEventTypeAsync(CreateEventType eventType);
        public Task<EventType?> UpdateEventTypeAsync(Guid id , EventType eventType );
        public Task<bool> DeleteEventTypeAsync(Guid id);
        public Task<EventType?> GetEventTypeByIdAsync(Guid id);
        public Task<EventType?> UpdateEventTypePartiallyAsync (Guid id , EventType eventType);
        public Task<bool> EventTypeExistsByIdAsync(Guid? id);
    }
}

using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IEventsService
    {
        public Task<IEnumerable<Event>> GetEventsAsync();
        public Task CreateEventAsync(CreateEvent createEvent);
        public Task<Event?> UpdateEventAsync(Guid id, Event updateEvent);
        public Task<Event?> UpdatePartiallyEventAsync(Guid id, Event updateEvent);
        public Task<bool> DeleteEventAsync(Guid id);
        public Task<Event?> GetEventByIdAsync(Guid id);
    }
}

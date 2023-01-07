using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IEventsService
    {
        public Task<IEnumerable<Event>> GetEvents();
        public Task CreateEvent(CreateEvent events);
        public Task<Event?> UpdateEvent(Guid id, Event events);
        public Task<Event?> UpdatePartiallyEvent(Guid id, Event events);
        public Task<bool> DeleteEvent(Guid id);
        public Task<Event?> GetEventById(Guid id);
        Task<bool> EventExistByIdAsync(Guid? id);
    }
}

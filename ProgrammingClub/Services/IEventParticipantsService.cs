using ProgrammingClub.Models;
using ProgrammingClub.DataContext;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IEventParticipantsService
    {
        public Task<IEnumerable<EventsParticipant>> GetEventsParticipantsAsync();
        public Task<IEnumerable<EventsParticipant>> GetEventsParticipantsByEventAndPaidAsync(Guid eventId, bool isPaid);
        public Task<IEnumerable<EventsParticipant>> GetEventsParticipantsByEventAndPresentAsync(Guid eventId, bool isPresent);
        public Task<IEnumerable<EventsParticipant>> GetAllParticipantsToEvent(Guid eventId);
        public Task CreateEventParticipant(CreateEventsParticipant participant);
        public Task<bool> DeleteEventParticipant(Guid idEventParticipant);
        public Task<EventsParticipant?> UpdateEventParticipant(Guid idEventParticipant, CreateEventsParticipant eventParticipant);
        public Task<EventsParticipant?> UpdateEventParticipantPartially(Guid idEventParticipant, EventsParticipant participant);
        public Task<EventsParticipant?> GetEventParticipantById(Guid id);
        public Task<bool> EventParticipantExists(Guid id);

    }
}

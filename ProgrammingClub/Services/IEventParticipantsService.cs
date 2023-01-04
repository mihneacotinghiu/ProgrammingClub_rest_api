using ProgrammingClub.Models;
using ProgrammingClub.DataContext;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IEventParticipantsService
    {
        public Task<IEnumerable<EventsParticipant>> GetEventsParticipantsAsync();
        public Task CreateEventParticipant(CreateEventsParticipant participant);
        public Task<bool> DeleteEventParticipant(Guid idEventParticipant);
        public Task<EventsParticipant?> UpdateEventParticipant(Guid idEventParticipant, EventsParticipant eventParticipant);
        public Task<EventsParticipant?> UpdateEventParticipantPartially(Guid idEventParticipant, EventsParticipant participant);
        public Task<EventsParticipant?> GetEventParticipantById(Guid id);
        public Task<bool> EventParticipantExists(Guid id);
        public Task<bool> UpdateEventParticipantPaid(Guid id);
        public Task<bool> UpdateEventParticipantPresent(Guid id);

    }
}

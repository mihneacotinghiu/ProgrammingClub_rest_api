using ProgrammingClub.Models;
using ProgrammingClub.DataContext;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IEventParticipantsService
    {
        public Task<IEnumerable<EventParticipant>> GetEventsParticipantsAsync();
        public Task CreateEventParticipant(CreateEventParticipant participant);
        public Task<bool> DeleteEventParticipant(Guid idEventParticipant);
        public Task<EventParticipant?> UpdateEventParticipant(Guid idEventParticipant, EventParticipant eventParticipant);
        public Task<EventParticipant?> UpdateEventParticipantPartially(Guid idEventParticipant, EventParticipant participant);
        public Task<EventParticipant?> GetEventParticipantById(Guid id);
        public Task<bool> EventParticipantExists(Guid id);
        public Task<bool> UpdateEventParticipantPaid(Guid id);
        public Task<bool> UpdateEventParticipantPresent(Guid id);

    }
}

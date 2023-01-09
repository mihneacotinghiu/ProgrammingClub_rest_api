using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingClub.Services
{
    public interface IModeratorService
    {
        public Task<IEnumerable<Moderator>> GetModerators();
        public Task CreateModerator(CreateModerator moderator);
        public Task<bool> DeleteModerator(Guid id);
        public Task<Moderator?> UpdateModerator(Guid idModerator, Moderator moderator);
        public Task<Moderator?> UpdatePartiallyModerator(Guid idModerator,Moderator moderator);

        public Task<Moderator?> GetModeratorById(Guid id);
    }
}

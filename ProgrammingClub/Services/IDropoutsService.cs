using ProgrammingClub.Models.CreateModerator;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IDropoutsService
    {
        public Task<IEnumerable<Dropout>> GetDropouts();
        public Task CreateDropout(Dropout dropout);
        public Task<bool> DeleteDropout(Guid id);
        public Task<Dropout?> UpdateDropout(Guid idDropout, Dropout dropout);
        public Task<Dropout?> UpdatePartiallyModerator(Guid idDropout, Dropout dropout);

        public Task<Dropout?> GetDropoutById(Guid id);
    }
}


using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Repositories
{
    public interface IDropoutsRepository
    {
        public Task CreateDropout(Dropout newDropout);
        public Task<Dropout?> GetDropoutByEventID(Guid eventId);
    }
}

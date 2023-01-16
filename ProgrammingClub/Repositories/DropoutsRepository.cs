using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Repositories
{
    public class DropoutsRepository : IDropoutsRepository
    {
        private readonly ProgrammingClubDataContext _context;

        public DropoutsRepository(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public async Task CreateDropout(Dropout newDropout)
        {
            _context.Entry(newDropout).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Dropout?> GetDropoutByEventID(Guid eventID)
        {
            return await _context.Dropouts.FirstOrDefaultAsync(m => m.IDEvent == eventID);
        }
    }
}

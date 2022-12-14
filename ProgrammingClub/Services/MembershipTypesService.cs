using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class MembershipTypesService : IMembershipTypesService
    {

        public readonly ProgrammingClubDataContext _context;

        public MembershipTypesService(ProgrammingClubDataContext context)
        {
            _context = context;
        
        }
        public async Task CreateMembershiptTypeAsync(MembershipType membershipType)
        {
            membershipType.IdMembershipType = Guid.NewGuid();
            _context.Entry(membershipType).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }


        public async Task<DbSet<MembershipType>> GetMembershipTypesAsync() //QUESTION------------------------------
        {
            return _context.MembershipTypes;
        }

        public async Task UpdateMembershiptTypeAsync(MembershipType membershipType)
        {
            _context.Update(membershipType);
            await _context.SaveChangesAsync();
        }

      

        public async Task<bool> DeleteMembershiptTypeAsync(Guid idMembershiptype)
        {
            MembershipType? membershipType = await GetMemberByIdAsync(idMembershiptype);
            if (membershipType == null) { return false; }

            _context.MembershipTypes.Remove(membershipType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MembershipType?> GetMemberByIdAsync(Guid idMembershiptype)
        {
            return await _context.MembershipTypes.SingleOrDefaultAsync(m => m.IdMembershipType == idMembershiptype);
        }

    }
}

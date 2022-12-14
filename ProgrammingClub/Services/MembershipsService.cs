using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class MembershipsService : IMembershipsService
    {
        private readonly ProgrammingClubDataContext _context;

        public MembershipsService(ProgrammingClubDataContext context)
        {
            _context = context;

        }

        public async Task CreateMembershipAsync(Membership membership)
        {
            try
            {
                membership.IdMembership = Guid.NewGuid();
                _context.Entry(membership).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
     
        }

        public async Task<bool> DeleteMembershipAsync(Guid id)
        {
            Membership? membership = await GetMembershipByIdAsync(id);
            if (membership == null)
                return false;
            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();  
            return true;
        }

       

        public Task<Membership?> GetMembershipByIdAsync(Guid id)
        {
            return _context.Memberships.FirstOrDefaultAsync(m => m.IdMembership == id);
        }

        public async Task<DbSet<Membership>> GetMembershipsAsync() 
        {

            return _context.Memberships;
        }

        public async Task UpdateMembershipAsync(Membership membership)
        {
            _context.Update(membership);
            await _context.SaveChangesAsync();
        }

        public async Task memberExist(Guid id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.IdMember == id);
            if (member == null)
                throw new Exception("member does not exist");
        }

        public async Task membershipTypeExist(Guid id)
        {
            var membershipType = await _context.MembershipTypes.FirstOrDefaultAsync(m => m.IdMembershipType == id);
            if (membershipType == null)
                throw new Exception("member does not exist");
        }

    }
}

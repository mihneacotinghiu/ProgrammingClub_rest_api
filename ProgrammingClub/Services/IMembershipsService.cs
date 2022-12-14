using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IMembershipsService
    {
        public Task<DbSet<Membership>> GetMembershipsAsync();
        public Task CreateMembershipAsync(Membership membership); 
        public Task UpdateMembershipAsync(Membership membership);
        public Task<bool> DeleteMembershipAsync(Guid id);
        public Task<Membership?> GetMembershipByIdAsync(Guid id);
        public Task membershipTypeExist(Guid id);
        public Task memberExist(Guid id);
    }
}

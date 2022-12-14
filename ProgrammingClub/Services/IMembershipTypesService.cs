using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IMembershipTypesService
    {
        public Task<DbSet<MembershipType>> GetMembershipTypesAsync();
        public Task CreateMembershiptTypeAsync(MembershipType membershipType);
        public Task<bool> DeleteMembershiptTypeAsync(Guid idMembershiptype);
        public Task UpdateMembershiptTypeAsync(MembershipType membershipType);
        public Task<MembershipType?> GetMemberByIdAsync(Guid idMembershiptype);

    }
}

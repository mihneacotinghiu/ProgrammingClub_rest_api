using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IMembershipTypesSerivce
    {
        public Task<DbSet<MembershipType>> GetMembershipTypesAsync();
        public Task CreateMembershipTypeAsync(MembershipType membershipType);
        public Task DeleteMembershipTypeAsync(MembershipType membershipType);
        public Task UpdateMembershipTypeAsync(MembershipType membershipType);
    }
}

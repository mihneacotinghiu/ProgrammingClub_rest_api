using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IMembersService
    {
        public Task<IEnumerable<Member>> GetMembers();
        public Task CreateMember(CreateMember member);
        public Task<bool> DeleteMember(Guid id);
        public Task<Member?> UpdateMember(Guid idMember, Member member);
        public Task<Member?> UpdatePartiallyMember(Guid idMember, Member member);

        public Task<Member?>GetMemberById(Guid id);
        public Task<bool> MemberExistByIdAsync(Guid id);
    }
}

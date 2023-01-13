using System.Net;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public class MembersService : IMembersService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMapper _mapper;

        public MembersService(ProgrammingClubDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteMember(Guid id)
        {
            if (!await MemberExistByIdAsync(id))
                return false;
            _context.Members.Remove(new Member { IdMember = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }
        public async Task CreateMember(CreateMember member)
        {
            var newMember = _mapper.Map<Member>(member);
            newMember.IdMember = Guid.NewGuid();
            var name = member.Name;
            _context.Entry(newMember).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Member?> UpdatePartiallyMember(Guid idMember, Member member)
        {

            var memberFromDatabase = await GetMemberById(idMember);
            if (memberFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(member.Name))
            {
                memberFromDatabase.Name = member.Name;
            }
            if (member.Resume != null)
            {
                memberFromDatabase.Resume = member.Resume;
            }
            if (member.Title != null)
            {
                memberFromDatabase.Title = member.Title;
            }
            if (member.Description != null)
            {
                memberFromDatabase.Description = member.Description;
            }
            if (member.Position != null)
            {
                memberFromDatabase.Position = member.Position;
            }

            _context.Update(member);
            await _context.SaveChangesAsync();
            return memberFromDatabase;
        }

        public async Task<Member?> UpdateMember(Guid idMember, Member member)
        {
            if (!await MemberExistByIdAsync(idMember))
            {
                return null;
            }
            member.IdMember = idMember;
            _context.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member?> GetMemberById(Guid id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.IdMember == id);
        }

        public async Task<bool> MemberExistByIdAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return false;
            }
            return await _context.Members.AnyAsync(m => m.IdMember == id);
        }

    }
}

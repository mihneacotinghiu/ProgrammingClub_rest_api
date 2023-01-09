using System.Diagnostics.Metrics;
using System.Net;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModerator;
using Member = ProgrammingClub.Models.Member;

namespace ProgrammingClub.Services
{
    public class ModeratorsService : IModeratorService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;
        public ModeratorsService(
            ProgrammingClubDataContext context,
            IMembersService membersService,
            IMapper mapper)
        {
            _context = context;
            _membersService = membersService;
            _mapper = mapper;
        }

        public async Task CreateModerator(CreateModerator moderator)
        {
            if (!await _membersService.MemberExistByIdAsync(moderator.IDMember))
            {
                throw new Exception("Member id not found! ");
            }

            if (await GetModeratorByMemberID(moderator.IDMember) != null)
            {
                throw new Exception("This Moderator already exists");
            }
            var newModerator = _mapper.Map<Moderator>(moderator);
            newModerator.IDModerator = Guid.NewGuid();
            _context.Entry(newModerator).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteModerator(Guid id)
        {
            if (!await ModeratorExistByIdAsync(id))
                return false;

            _context.Moderators.Remove(new Moderator { IDModerator = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Moderator?> GetModeratorById(Guid id)
        {
            return await _context.Moderators.FirstOrDefaultAsync(m => m.IDModerator == id);
        }

        public async Task<Moderator?> GetModeratorByMemberID(Guid? memberId)
        {
            if (memberId == null)
                return null;
            return await _context.Moderators.FirstOrDefaultAsync(m => m.IDMember == memberId);
        }

        public async Task<IEnumerable<Moderator>> GetModerators()
        {
             return await _context.Moderators.ToListAsync();
        }

        public async Task<Moderator?> UpdateModerator(Guid IDModerator, Moderator moderator)
        {
            await ValidateModerator(IDModerator, moderator);
            moderator.IDModerator = IDModerator;
            _context.Update(moderator);
            await _context.SaveChangesAsync();
            return moderator;
        }

        private async Task ValidateModerator(Guid IDModerator, Moderator moderator)
        {
            if (!await ModeratorExistByIdAsync(IDModerator))
            {
                throw new Exception("IDModerator not found! ");
            }
            if (moderator.Description == null)
            {
                throw new Exception("Description cannot be null! ");
            }
            if (moderator.Title == null)
            {
                throw new Exception("Title cannot be null! ");
            }
            if (!await _membersService.MemberExistByIdAsync(moderator.IDMember))
            {
                throw new Exception("Member id not found! ");
            }
        }

        public async Task<Moderator?> UpdatePartiallyModerator(Guid IDModerator, Moderator moderator)
        {
            var moderatorFromDatabase = await GetModeratorById(IDModerator);
            if (moderatorFromDatabase == null)
            {
                return null;
            }
            bool needUpdate = false;

            if (!string.IsNullOrEmpty(moderator.Title) && moderatorFromDatabase.Title != moderator.Title)
            {
                moderatorFromDatabase.Title = moderator.Title;
                needUpdate = true;
                
            }
            if (moderator.Description != null && moderatorFromDatabase.Description != moderator.Description)
            {
                moderatorFromDatabase.Description = moderator.Description;
                needUpdate = true;
            }
            if (moderator.IDMember.HasValue && moderatorFromDatabase.IDMember != moderator.IDMember)
            {
                if (GetModeratorByMemberID(moderator.IDMember) != null)
                {
                    throw new Exception("This Moderator already exists");
                }
                moderatorFromDatabase.IDMember = moderator.IDMember;
                needUpdate = true;
            }
            if (needUpdate)
            {
                await ValidateModerator(IDModerator, moderatorFromDatabase);
                _context.Update(moderatorFromDatabase);
                await _context.SaveChangesAsync();
            }
            return moderatorFromDatabase;
        }

        public async Task<bool> ModeratorExistByIdAsync(Guid id)
        {
            return await _context.Moderators.CountAsync(moderator => moderator.IDModerator == id) > 0;
        }
    }
}

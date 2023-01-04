using System.Diagnostics.Metrics;
using System.Net;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using Member = ProgrammingClub.Models.Member;

namespace ProgrammingClub.Services
{
    public class ModeratorsService : IModeratorService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;
        public ModeratorsService(ProgrammingClubDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateModerator(Moderator moderator)
        {
         
             //   Member? member = await _membersService.GetMemberById((Guid)moderator.IDMember);
             //   if (member!= null)
             //   {
                    var newModerator = _mapper.Map<Moderator>(moderator);
                    newModerator.IDModerator = Guid.NewGuid();
                    _context.Entry(newModerator).State = EntityState.Added;
                    await _context.SaveChangesAsync();
              //  }
        
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

        public async Task<IEnumerable<Moderator>> GetModerators()
        {
            return _context.Moderators.ToList();
        }

        public async Task<Moderator?> UpdateModerator(Guid IDModerator, Moderator moderator)
        {
            if (!await ModeratorExistByIdAsync(IDModerator))
            {
                return null;
            }
            moderator.IDModerator = IDModerator;
            _context.Update(moderator);
            await _context.SaveChangesAsync();
            return moderator;
        }

        public async Task<Moderator?> UpdatePartiallyModerator(Guid IDModerator, Moderator moderator)
        {
            var moderatorFromDatabase = await GetModeratorById(IDModerator);
            if (moderatorFromDatabase == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(moderator.Title))
            {
                moderatorFromDatabase.Title = moderator.Title;
            }
            if (moderator.Description != null)
            {
                moderatorFromDatabase.Description = moderator.Description;
            }

            _context.Update(moderator);
            await _context.SaveChangesAsync();
            return moderatorFromDatabase;
        }

        public async Task<bool> ModeratorExistByIdAsync(Guid id)
        {
            return await _context.Moderators.CountAsync(moderator => moderator.IDModerator == id) > 0;
        }
    }
}

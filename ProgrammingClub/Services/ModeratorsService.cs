using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;


namespace ProgrammingClub.Services
{
    public class ModeratorsService : IModeratorService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;
        public ModeratorsService(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task CreateModerator(Moderator moderator)
        {
            moderator.IDModerator = Guid.NewGuid();
            Moderator newModerator = new Moderator
            {
                IDModerator = Guid.NewGuid(),
                Title = moderator.Title,
                Description = moderator.Description,
                IDMember= moderator.IDMember,
            };
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

        public async Task<Moderator?> UpdatePartiallyModerator(Guid IDModerator, Guid IdMember, Moderator moderator)
        {
            var moderatorFromDatabase = await GetModeratorById(IDModerator);
            if (moderatorFromDatabase == null)
            {
                return null;
            }
            var memberFromDatabase = await _membersService.GetMemberById(IdMember);
            if (memberFromDatabase == null)
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
            return await _context.Moderators.CountAsync(m => m.IDModerator == id) > 0;
        }

        public Task<Moderator?> UpdatePartiallyModerator(Guid idModerator, Moderator moderator)
        {
            throw new NotImplementedException();
        }
    }
}

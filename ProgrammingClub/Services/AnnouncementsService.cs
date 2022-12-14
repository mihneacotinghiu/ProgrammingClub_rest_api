using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;


namespace ProgrammingClub.Services
{
    public class AnnouncementsService : IAnnouncementsService
    {

        private readonly ProgrammingClubDataContext _context;

        public AnnouncementsService(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public async Task CreateAnnouncementAsync(Announcement announcement)
        {
               announcement.IdAnnouncement = Guid.NewGuid();
               _context.Entry(announcement).State = EntityState.Added;
               await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            Announcement? announcement = await GetAnnounctmentByIdAsync(id);
            if (announcement == null)
                return false;
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DbSet<Announcement>> GetAnnouncementsAsync()
        {
            return _context.Announcements;
        }

        public async Task<Announcement?> GetAnnounctmentByIdAsync(Guid id)
        {
            return await _context.Announcements.FirstOrDefaultAsync(a => a.IdAnnouncement== id);
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            _context.Update(announcement);
            await _context.SaveChangesAsync();
        }
    }
}

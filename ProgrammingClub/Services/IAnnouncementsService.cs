using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IAnnouncementsService
    {
        public Task<DbSet<Announcement>> GetAnnouncementsAsync();
        public Task CreateAnnouncementAsync(Announcement announcement);
        public Task UpdateAnnouncementAsync(Announcement announcement);
        public Task<bool> DeleteAnnouncementAsync(Guid id);
        public Task<Announcement?> GetAnnounctmentByIdAsync(Guid id);
    }
}

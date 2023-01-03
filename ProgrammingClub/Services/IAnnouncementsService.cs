using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface IAnnouncementsService
    {
        public Task<IEnumerable<Announcement>> GetAnnouncementsAsync();
        public Task CreateAnnouncementAsync(CreateAnnouncement announcement);
        public Task<Announcement?> UpdateAnnouncementAsync(Guid id ,Announcement announcement);
        public Task<Announcement?> UpdatePartiallyAnnouncementAsync(Guid id, Announcement announcement);
        public Task<bool> DeleteAnnouncementAsync(Guid id);
        public Task<Announcement?> GetAnnounctmentByIdAsync(Guid id);
        public Task AnnouncementIsValid(Announcement announcement);
    }
}

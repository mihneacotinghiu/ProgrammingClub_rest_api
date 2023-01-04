using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMapper _mapper;

        public AnnouncementsService(ProgrammingClubDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAnnouncementAsync(CreateAnnouncement announcement)
        {
            Helpers.ValidationFunctions.TrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);

            var newAnnouncement = _mapper.Map<Announcement>(announcement);
            newAnnouncement.IdAnnouncement = Guid.NewGuid();
            _context.Entry(newAnnouncement).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            if (!await ExistAnnounctmenAsync(id)) { return false; }

            _context.Announcements.Remove(new Announcement { IdAnnouncement= id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement?> GetAnnounctmentByIdAsync(Guid id)
        {
           return await _context.Announcements.SingleOrDefaultAsync(a=>a.IdAnnouncement == id);
        }

        public async Task<bool> ExistAnnounctmenAsync(Guid id) 
        {
            return await _context.Announcements.CountAsync(a => a.IdAnnouncement == id) > 0 ;
        }

        public async Task<Announcement?> UpdateAnnouncementAsync(Guid id , Announcement announcement)
        {
            Helpers.ValidationFunctions.TrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);
            
            if (!await ExistAnnounctmenAsync(id)) { return null; }

            announcement.IdAnnouncement = id;
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement?> UpdatePartiallyAnnouncementAsync(Guid id, Announcement announcement)
        {
            Helpers.ValidationFunctions.TrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);

            var announcementFromDatabase = await GetAnnounctmentByIdAsync(id);
            if (announcementFromDatabase == null) { return null; }

            if (announcement.Tags != null) { announcementFromDatabase.Tags = announcement.Tags; }
            if (announcement.ValidFrom.HasValue) { announcementFromDatabase.ValidFrom = announcement.ValidFrom; }
            if (announcement.ValidTo.HasValue) { announcementFromDatabase.ValidTo = announcement.ValidTo; }
            if (announcement.Text != null) { announcementFromDatabase.Text = announcement.Text; }
            if (announcement.Title != null) { announcementFromDatabase.Title = announcement.Title; }
            if (announcement.EventDate.HasValue) { announcementFromDatabase.EventDate= announcement.EventDate; }

            _context.Update(announcementFromDatabase);
            await _context.SaveChangesAsync();
            return announcementFromDatabase;
        }
    }
}

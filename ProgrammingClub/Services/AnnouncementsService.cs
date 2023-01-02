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
            var newAnnouncement = _mapper.Map<Announcement>(announcement);
            await AnnouncementIsValid(newAnnouncement);
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
            return _context.Announcements.ToList();
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
            if (!await ExistAnnounctmenAsync(id)) { return null; }

            await AnnouncementIsValid(announcement);

            announcement.IdAnnouncement = id;
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement?> UpdatePartiallyAnnouncementAsync(Guid id, Announcement announcement)
        {
            if (!await ExistAnnounctmenAsync(id)) { return null; }

            var announcementFromDatabase = await GetAnnounctmentByIdAsync(id);
            if (announcementFromDatabase == null) { return null; }

            if (announcement.Tags != null) { announcementFromDatabase.Tags = announcement.Tags; }
            if (announcement.ValidFrom != null) { announcementFromDatabase.ValidFrom = announcement.ValidFrom; }
            if (announcement.ValidTo != null) { announcementFromDatabase.ValidTo = announcement.ValidTo; }
            if (announcement.Text != null) { announcementFromDatabase.Text = announcement.Text; }
            if (announcement.Title != null) { announcementFromDatabase.Title = announcement.Title; }
            if (announcement.EventDate != null) { announcementFromDatabase.EventDate= announcement.EventDate; }

            await AnnouncementIsValid(announcementFromDatabase);
            _context.Update(announcementFromDatabase);
            await _context.SaveChangesAsync();
            return announcementFromDatabase;

        }

        public async Task AnnouncementIsValid(Announcement announcement)
        {
            var validDates = Helpers.ValidationFunctions.TwoDatesValidator(announcement.ValidFrom, announcement.ValidTo);

            await Task.WhenAll(validDates);
        }
    }
}

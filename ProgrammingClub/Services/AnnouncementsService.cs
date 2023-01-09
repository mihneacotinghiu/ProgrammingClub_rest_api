using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Exceptions;
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
            if (!await ExistAnnounctmenAsync(id)) 
            {
                return false; 
            }
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
            if (!await ExistAnnounctmenAsync(id)) 
            {
                return null;
            }
            announcement.IdAnnouncement = id;
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement?> UpdatePartiallyAnnouncementAsync(Guid id, Announcement announcement)
        {
            
            bool announcementIsChanged = false, dateIsChanged = false;
            var announcementFromDatabase = await GetAnnounctmentByIdAsync(id);
            announcement.IdAnnouncement = id;

            if (announcementFromDatabase == null) 
            {
                return null;
            }
            if (!string.IsNullOrEmpty(announcement.Tags)  && announcement.Tags != announcementFromDatabase.Tags) 
            {
                announcementFromDatabase.Tags = announcement.Tags;
                announcementIsChanged = true;
            }
            if (announcement.ValidFrom.HasValue && announcement.ValidFrom != announcementFromDatabase.ValidFrom) 
            {
                announcementFromDatabase.ValidFrom = announcement.ValidFrom;
                announcementIsChanged = true;
                dateIsChanged= true;
            }
            if (announcement.ValidTo.HasValue && announcement.ValidTo != announcementFromDatabase.ValidTo) 
            {
                announcementFromDatabase.ValidTo = announcement.ValidTo;
                announcementIsChanged = true;
                dateIsChanged= true;
            }
            if (!string.IsNullOrEmpty(announcement.Text) && announcement.Text != announcementFromDatabase.Text) 
            {
                announcementFromDatabase.Text = announcement.Text;
                announcementIsChanged = true;
            }
            if (!string.IsNullOrEmpty(announcement.Title) && announcement.Title != announcementFromDatabase.Title) 
            {
                announcementFromDatabase.Title = announcement.Title;
                announcementIsChanged = true;
            }
            if (announcement.EventDate.HasValue && announcement.EventDate != announcementFromDatabase.EventDate) 
            {
                announcementFromDatabase.EventDate= announcement.EventDate;
                announcementIsChanged = true;
            }

            if (!announcementIsChanged)
            {
                throw new ModelValidationException(Helpers.ErrorMessegesEnum.ZeroUpdatesToSave);
            }
            if(dateIsChanged) 
            {
                Helpers.ValidationFunctions.TrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);
            }
            _context.Update(announcementFromDatabase);
            await _context.SaveChangesAsync();
            return announcementFromDatabase;
        }
    }
}

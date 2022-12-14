using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System.Diagnostics.Metrics;
using System.Net;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementsService _announcementsService;

        public AnnouncementsController(IAnnouncementsService announcementsService)
        {
            _announcementsService = announcementsService;
        }

        [Route("GetAnnouncements")]
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            try
            {
                DbSet<Announcement> announcements = await _announcementsService.GetAnnouncementsAsync();
                if (announcements != null && announcements.ToList().Count > 0)
                    return Ok(announcements);

                return StatusCode((int)HttpStatusCode.NoContent,ErrorMessegesEnum.NoElementFound);
            }
            catch (Exception ex) {return StatusCode((int)HttpStatusCode.InternalServerError, ex);}
            
           
            
        }


        [Route("GetAnnouncement")]
        [HttpGet]
        public async Task<IActionResult> GetAnnouncement([FromQuery]Guid id)
        {
            try
            {
                var announcment = await _announcementsService.GetAnnounctmentByIdAsync(id);
                if (announcment != null)
                    return Ok(announcment);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }



        }
        [Route("PostAnnouncements")]
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement announcement)
        {
     
            try
            {
                if (announcement != null)
                {

                    await _announcementsService.CreateAnnouncementAsync(announcement);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
            
        }

        [Route("DeleteAnnouncements")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncement([FromQuery] Guid id)
        {

            try
            {
                var result = await _announcementsService.DeleteAnnouncementAsync(id);
                if (result)
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);

                return StatusCode((int)HttpStatusCode.BadRequest);

                
            }
            catch (Exception ex){return StatusCode((int)HttpStatusCode.InternalServerError, ex);}
                
        }

        [Route("PutAnnouncements")]
        [HttpPatch]
        public async Task<IActionResult> UpdateAnnouncement(Guid idAnnouncement,[FromBody] Announcement announcement) {

            try
            {
                if (announcement != null)
                {
                    var announcementFromDatabase =  await _announcementsService.GetAnnounctmentByIdAsync(idAnnouncement);

                    if (announcement.ValidFrom != null)
                        announcementFromDatabase.ValidFrom = announcement.ValidFrom;

                    if (announcement.ValidTo != null)
                        announcementFromDatabase.ValidTo = announcement.ValidTo;

                    if (announcement.Title != null)
                        announcementFromDatabase.Title = announcement.Title;

                    if (announcement.Tags != null)
                        announcementFromDatabase.Tags = announcement.Tags;

                    if (announcement.EventDate != null)
                        announcementFromDatabase.EventDate = announcement.EventDate;

                    await _announcementsService.UpdateAnnouncementAsync(announcementFromDatabase);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }




    }
}

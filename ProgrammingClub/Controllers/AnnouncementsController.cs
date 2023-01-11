using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Exceptions;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Diagnostics.Metrics;
using System.Net;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementsService _announcementsService;

        public AnnouncementsController(IAnnouncementsService announcementsService)
        {
            _announcementsService = announcementsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            try
            {
                var announcements = await _announcementsService.GetAnnouncementsAsync();
                if (announcements == null || !announcements.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(announcements);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement([FromRoute] Guid id)
        {
            try
            {
                var announcement = await _announcementsService.GetAnnounctmentByIdAsync(id);
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody] CreateAnnouncement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                await _announcementsService.CreateAnnouncementAsync(announcement);
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]Guid id)
        {
            try
            {
               var result = await _announcementsService.DeleteAnnouncementAsync(id);
                if (result)
                {
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                return StatusCode((int)HttpStatusCode.BadRequest,Helpers.ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute]Guid id , [FromBody] CreateAnnouncement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedAnnoucement = await _announcementsService.UpdateAnnouncementAsync(id, announcement);
                if(updatedAnnoucement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAnnouncement([FromRoute] Guid id, [FromBody] Announcement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedAnnoucement = await _announcementsService.UpdatePartiallyAnnouncementAsync(id, announcement);
                if (updatedAnnoucement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }




    }
}

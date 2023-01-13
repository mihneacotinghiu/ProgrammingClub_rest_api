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
        private readonly ILogger<AnnouncementsController> _logger;

        public AnnouncementsController(
            IAnnouncementsService announcementsService,
            ILogger<AnnouncementsController> logger
            )
        {
            _announcementsService = announcementsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            try
            {
                _logger.LogInformation("GetAnnouncements start");
                var announcements = await _announcementsService.GetAnnouncementsAsync();
                _logger.LogInformation($"GetAnnouncements end,total result: {announcements.Count()}");
                if (announcements == null || !announcements.Any())
                {
                    _logger.LogInformation("GetAnnouncements no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAnnouncements error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation($"GetAnnouncement start,id: {id}");
                var announcement = await _announcementsService.GetAnnounctmentByIdAsync(id);
                _logger.LogInformation("GetAnnouncement end");
                if (announcement == null)
                {
                    _logger.LogInformation("GetAnnouncement no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"GetAnnouncement error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody] CreateAnnouncement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    _logger.LogInformation("PostAnnouncement FromBody announcement is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PostAnnouncement start , {announcement.GetLoggingInfo()}");
                await _announcementsService.CreateAnnouncementAsync(announcement);
                _logger.LogInformation("PostAnnouncement end");
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PostAnnouncement validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PostAnnouncement error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]Guid id)
        {
            try
            {
                _logger.LogInformation($"DeleteAnnouncement start , id: {id}");
               var result = await _announcementsService.DeleteAnnouncementAsync(id);
                _logger.LogInformation("DeleteAnnouncement end");
                if (result)
                {
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                _logger.LogInformation("DeleteAnnouncement no elem found");
                return StatusCode((int)HttpStatusCode.BadRequest,Helpers.ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"DeleteAnnouncement error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute]Guid id , [FromBody] CreateAnnouncement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    _logger.LogInformation("PutAnnouncement FromBody announcement is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                announcement.IdAnnouncement= id;
                _logger.LogInformation($"PutAnnouncement starts {announcement.GetLoggingInfo()}");
                var updatedAnnoucement = await _announcementsService.UpdateAnnouncementAsync(id, announcement);
                _logger.LogInformation("PutAnnouncement ends");
                if(updatedAnnoucement == null)
                {
                    _logger.LogInformation("PutAnnouncement no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PutAnnouncement validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PutAnnouncement error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAnnouncement([FromRoute] Guid id, [FromBody] Announcement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    _logger.LogInformation("PatchAnnouncement FromBody announcement is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PatchAnnouncement starts, id {id}, announcement: {announcement.GetLoggingInfo()}");
                var updatedAnnoucement = await _announcementsService.UpdatePartiallyAnnouncementAsync(id, announcement);
                _logger.LogInformation($"PatchAnnouncement end");
                if (updatedAnnoucement == null)
                {
                    _logger.LogInformation("PathAnnouncement no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PatchAnnouncement validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PatchAnnouncement error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

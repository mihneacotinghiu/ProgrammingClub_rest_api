using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Exceptions;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Net;
using static ProgrammingClub.Helpers.ErrorMessagesEnum;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IEventsService eventsService, ILogger<EventsController> logger)
        {
            _eventsService = eventsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                _logger.LogInformation("GetEvents start");
                var events = await _eventsService.GetEventsAsync();
                _logger.LogInformation($"GetEvents end, total results: {events.Count()}");
                if(events == null || !events.Any()) 
                {
                    _logger.LogInformation("No events found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(events);
            }
            catch (Exception ex) {
                _logger.LogError($"GetEvents error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation($"GetEventById start, id: {id}");
                var getEvent = await _eventsService.GetEventByIdAsync(id);
                _logger.LogInformation("GetEventById end");
                if (getEvent == null)
                {
                    _logger.LogInformation("The event does not exist");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(getEvent);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"GetEventById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] CreateEvent createEvent)
        {
            try
            {
                if(createEvent == null)
                {
                    _logger.LogInformation("PostEvent FromBody createEvent is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PostEvent start , {createEvent.GetLoggingInfo()}");
                await _eventsService.CreateEventAsync(createEvent);
                _logger.LogInformation("PostEvent end");
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PostEvent validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PostEvent error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation($"DeleteEvent start , id: {id}");
                var result = await _eventsService.DeleteEventAsync(id);
                _logger.LogInformation("DeleteEvent end");
                if (result)
                {
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                _logger.LogInformation("DeleteEvent no event found to delete");
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"DeleteEvent error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] Guid id, [FromBody] CreateEvent updateEvent)
        {
            try
            {
                if(updateEvent == null)
                {
                    _logger.LogInformation("PutEvent FromBody updateEvent is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PutEvent start , {updateEvent.GetLoggingInfo()}");
                var updatedEvent = await _eventsService.UpdateEventAsync(id, updateEvent);
                _logger.LogInformation("PutEvent end");
                if(updatedEvent == null)
                {
                    _logger.LogInformation("PutEvent no element found");
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PutEvent validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PutEvent error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEvent([FromRoute] Guid id, [FromBody] Event patchEvent)
        {
            try
            {
                if (patchEvent == null)
                {
                    _logger.LogInformation("PatchEvent FromBody updateEvent is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PatchEvent start , {patchEvent.GetLoggingInfo()}");
                var updatedEvent = await _eventsService.UpdatePartiallyEventAsync(id, patchEvent);
                _logger.LogInformation("PatchEvent end");

                if (updatedEvent == null)
                {
                    _logger.LogInformation("PatchEvent no element found");
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogWarning($"PatchEvent validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PatchEvent error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }
    }
}
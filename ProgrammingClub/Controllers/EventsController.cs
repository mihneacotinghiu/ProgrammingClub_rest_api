using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Exceptions;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Net;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _eventsService.GetEventsAsync();
                if(events == null || !events.Any()) {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(events);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            try
            {
                var getEvent = await _eventsService.GetEventByIdAsync(id);
                if (getEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(getEvent);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] CreateEvent createEvent)
        {
            try
            {
                if(createEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                await _eventsService.CreateEventAsync(createEvent);
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            try
            {
                var result = await _eventsService.DeleteEventAsync(id);
                if (result)
                {
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] Guid id, [FromBody] Event events)
        {
            try
            {
                if(events == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedEvent = await _eventsService.UpdateEventAsync(id, events);
                if(updatedEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEvent([FromRoute] Guid id, [FromBody] Event events)
        {
            try
            {
                if (events == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedEvent = await _eventsService.UpdatePartiallyEventAsync(id, events);
                if (updatedEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }
    }
}
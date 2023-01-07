using Microsoft.AspNetCore.Mvc;
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
                var events = await _eventsService.GetEvents();
                if(events== null || !events.Any()) {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }
                return Ok(events);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            try
            {
                Event? events = await _eventsService.GetEventById(id);
                if (events != null)
                {
                    return Ok(events);
                }
                return StatusCode((int)HttpStatusCode.NoContent, null);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] CreateEvent createEvent)
        {
            try
            {
                if(createEvent != null)
                {
                    await _eventsService.CreateEvent(createEvent);
                    return Ok();
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            try
            {
                var result = await _eventsService.DeleteEvent(id);
                if (result)
                {
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            return StatusCode((int)HttpStatusCode.BadRequest, "No element found");
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
                var updatedEvent = await _eventsService.UpdateEvent(id, events);
                if(updatedEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
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
                var updatedEvent = await _eventsService.UpdatePartiallyEvent(id, events);
                if (updatedEvent == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }
    }
}
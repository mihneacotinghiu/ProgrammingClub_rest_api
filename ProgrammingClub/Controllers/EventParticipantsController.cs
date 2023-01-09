using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics.Metrics;
using ProgrammingClub.Exceptions;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantsController : ControllerBase
    {
        private readonly IEventParticipantsService _eventParticipantsService;

        public EventParticipantsController(IEventParticipantsService eventParticipantsService)
        {
            _eventParticipantsService = eventParticipantsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventParticipants()
        {
            try
            {
                var eventParticipants = await _eventParticipantsService.GetEventsParticipantsAsync();
                if (eventParticipants == null || !eventParticipants.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(eventParticipants);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventParticipantByID([FromRoute] Guid id)
        {
            try
            {
                EventsParticipant? eventParticipant = await _eventParticipantsService.GetEventParticipantById(id);
                if (eventParticipant != null)
                {
                    return Ok(eventParticipant);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [HttpGet("event/{eventId}/ispaid/{isPaid}")]
        public async Task<IActionResult> GetEventParticipantByEventAndPaid([FromRoute] Guid eventId, [FromRoute] bool isPaid)
        {
            try
            {
                var eventParticipant = await _eventParticipantsService.GetEventsParticipantsByEventAndPaidAsync(eventId, isPaid);
                if (eventParticipant != null)
                {
                    return Ok(eventParticipant);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch 
            { 
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); 
            }
        }

        [HttpGet("event/{eventId}/ispresent/{isPresent}")]
        public async Task<IActionResult> GetEventParticipantByEventAndPresent([FromRoute] Guid eventId, [FromRoute]bool isPresent)
        {
            try
            {
                var eventParticipant = await _eventParticipantsService.GetEventsParticipantsByEventAndPresentAsync(eventId, isPresent);
                if(eventParticipant != null)
                {
                    return Ok(eventParticipant);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError);
            }
        }

        [HttpGet("event/{eventId}/")]
        public async Task<IActionResult> GetAllParticipantsToEvent([FromRoute] Guid eventId)
        {
            try
            {
                var eventParticipant = await _eventParticipantsService.GetAllParticipantsToEvent(eventId);
                if (eventParticipant != null)
                {
                    return Ok(eventParticipant);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch  { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventParticipant([FromBody] CreateEventsParticipant eventParticipant)
        {
            try
            {
                if (eventParticipant != null)
                {
                    await _eventParticipantsService.CreateEventParticipant(eventParticipant);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch(Exception ex)  
            { 
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventParticipant([FromQuery] Guid eventParticipantID)
        {
            try
            {
                if (await _eventParticipantsService.DeleteEventParticipant(eventParticipantID))
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);


            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
            return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.NoElementFound);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventParticipant([FromQuery]Guid idEventParticipant, [FromBody]EventsParticipant eventParticipant)
        {
            try
            {
                if (eventParticipant == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                var updatedEventParticipant = await _eventParticipantsService.UpdateEventParticipant(idEventParticipant, eventParticipant);
                if(updatedEventParticipant == null)
                {
                   return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ElementAlreadyExistsInDB ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEventParticipantPartially([FromQuery]Guid idEventParticipant, [FromBody]EventsParticipant eventParticipant)
        {
            try
            {
                if (eventParticipant == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.NoElementFound);
                }

                var updatedEventParticipant = await _eventParticipantsService.UpdateEventParticipantPartially(idEventParticipant, eventParticipant);
                if (updatedEventParticipant == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        

    }
}

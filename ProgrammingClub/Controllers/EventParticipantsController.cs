using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics.Metrics;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventParticipantsController : ControllerBase
    {
        private readonly IEventParticipantsService _eventParticipantsService;

        public EventParticipantsController(IEventParticipantsService eventParticipantsService)
        {
            _eventParticipantsService = eventParticipantsService;
        }

        [Route("GetEventParticipants")]
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

        [Route("GetEventParticipantByID")]
        [HttpGet]
        public async Task<IActionResult> GetEventParticipantByID([FromQuery] Guid participantId)
        {
            try
            {
                EventParticipant? eventParticipant = await _eventParticipantsService.GetEventParticipantById(participantId);
                if (eventParticipant != null)
                {
                    return Ok(eventParticipant);
                }
                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [Route("CreateEventParticipant")]
        [HttpPost]
        public async Task<IActionResult> CreateEventParticipant([FromBody] CreateEventParticipant eventParticipant)
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
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [Route("DeleteEventParticipant")]
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

        [Route("UpdateEventParticipant")]
        [HttpPut]
        public async Task<IActionResult> UpdateEventParticipant([FromQuery]Guid idEventParticipant, [FromBody]EventParticipant eventParticipant)
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
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [Route("UpdateEventParticipantPartially")]
        [HttpPatch]
        public async Task<IActionResult> UpdateEventParticipantPartially([FromQuery]Guid idEventParticipant, [FromBody]EventParticipant eventParticipant)
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
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [Route("UpdateEventParticipantPaid")]
        [HttpPatch]
        public async Task<IActionResult> UpdateEventParticipantPaid([FromQuery] Guid idEventParticipant)
        {
            try
            {
                var result = await _eventParticipantsService.UpdateEventParticipantPaid(idEventParticipant);
                if(result == false)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

        [Route("UpdateEventParticipantPresent")]
        [HttpPatch]
        public async Task<IActionResult> UpdateEventParticipantPresent([FromQuery] Guid idEventParticipant)
        {
            try
            {
                var result = await _eventParticipantsService.UpdateEventParticipantPresent(idEventParticipant);
                if (result == false)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); }
        }

    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorsController:ControllerBase
    {
        private readonly IModeratorService _moderatorsService;

        public ModeratorsController(IModeratorService moderatorsService)
        {
            _moderatorsService = moderatorsService;
        }


        [HttpPost]
        public async Task<IActionResult> PostModerator([FromBody] CreateModerator moderator)
        {
            try
            {
                if (moderator != null)
                {
                    await _moderatorsService.CreateModerator(moderator);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }



        [HttpGet]
        public async Task<IActionResult> GetModerators()
        {
            try
            {
                var moderators = await _moderatorsService.GetModerators();
                if (moderators == null || !moderators.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(moderators);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModeratorById([FromRoute] Guid id)
        {

            try
            {
                Moderator? moderator = await _moderatorsService.GetModeratorById(id);
                if (moderator != null)
                    return Ok(moderator);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModerator([FromRoute] Guid id)
        {
            try
            {
                var result = await _moderatorsService.DeleteModerator(id);
                if (result)
                {
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutModerator([FromRoute] Guid id, [FromBody] CreateModerator moderator)
        {
            try
            {
                if (moderator == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedModerator = await _moderatorsService.UpdateModerator(id, moderator);
                if (updatedModerator == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchModerator([FromRoute] Guid id, [FromBody] Moderator moderator)
        {
            try
            {
                if (moderator == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedModerator = await _moderatorsService.UpdatePartiallyModerator(id, moderator);
                if (updatedModerator == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

        }
    }
}

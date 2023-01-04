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
        public async Task<IActionResult> PostModerator([FromBody] Moderator moderator)
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
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(moderators);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
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
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutModerator([FromRoute] Guid IDModerator, [FromBody] Moderator moderator)
        {
            try
            {
                if (moderator == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedModerator = await _moderatorsService.UpdateModerator(IDModerator, moderator);
                if (updatedModerator == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMember([FromRoute] Guid IDModerator, [FromRoute] Guid IDMember, [FromBody] Moderator moderator)
        {
            try
            {
                if (moderator == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedModerator = await _moderatorsService.UpdatePartiallyModerator(IDModerator, IDMember, moderator);
                if (updatedModerator == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }
}

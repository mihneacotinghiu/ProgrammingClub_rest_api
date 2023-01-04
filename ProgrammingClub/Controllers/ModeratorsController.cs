using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Helpers;
using ProgrammingClub.Services;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModeratorsController:ControllerBase
    {
        private readonly IModeratorService _moderatorsService;

        public ModeratorsController(IModeratorService moderatorsService)
        {
            _moderatorsService = moderatorsService;
        }
        [Route("GetModerators")]
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
    }
}

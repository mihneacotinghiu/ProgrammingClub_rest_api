using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System.Net;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropoutsController : ControllerBase
    {
            private readonly IDropoutsService _dropoutsService;

            public DropoutsController(IDropoutsService dropoutsService)
            {
              _dropoutsService = dropoutsService;
            }


            [HttpPost]
            public async Task<IActionResult> PostDropout([FromBody] CreateDropout dropout)
            {
                try
                {
                    if (dropout != null)
                    {
                        await _dropoutsService.CreateDropout(dropout);
                        return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                    }
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
            }



            [HttpGet]
            public async Task<IActionResult> GetDropouts()
            {
                try
                {
                    var dropouts = await _dropoutsService.GetDropouts();
                    if (dropouts == null || !dropouts.Any())
                    {
                        return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                    }

                    return Ok(dropouts);
                }
                catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDropoutById([FromRoute] Guid id)
        {

            try
            {
                Dropout? dropout = await _dropoutsService.GetDropoutById(id);
                if (dropout != null)
                    return Ok(dropout);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteDropouts([FromRoute] Guid id)
            {
                try
                {
                    var result = await _dropoutsService.DeleteDropout(id);
                    if (result)
                    {
                        return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                    }
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
                catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutDropouts([FromRoute] Guid id, [FromBody] CreateDropout dropout)
            {
                try
                {
                    if (dropout == null)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest);
                    }

                    var UpdateDropout = await _dropoutsService.UpdateDropout(id, dropout);
                    if (UpdateDropout == null)
                    {
                        return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                    }

                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

            }

            [HttpPatch("{id}")]
            public async Task<IActionResult> PatchDropout([FromRoute] Guid id, [FromBody] Dropout dropout)
            {
                try
                {
                    if (dropout == null)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest);
                    }

                    var updatedDropout = await _dropoutsService.UpdatePartiallyDropout(id, dropout);
                    if (updatedDropout == null)
                    {
                        return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                    }

                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

            }
        }
    
}

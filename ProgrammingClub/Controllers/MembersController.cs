using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Data;
using System.Net;
using System.Net.WebSockets;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;
        private readonly ILogger<MembersController> _logger;

        public MembersController(
            IMembersService membersService,
            ILogger<MembersController> logger
            )
        {
            _membersService = membersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                _logger.LogError("GetMember start");
                var members = await _membersService.GetMembers();
                _logger.LogError($"GetMember end, total results: {members?.Count()}");
                if (members == null || !members.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(members);
            }
            catch (Exception ex) {
                _logger.LogError($"GetMembers error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById([FromRoute] Guid id)
        {
            try
            {
                _logger.LogError($"GetMemberById start, id: {id}");
                Member? member = await _membersService.GetMemberById(id);
                _logger.LogError($"GetMemberById end");
                if (member != null)
                    return Ok(member);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"GetMemberById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] CreateMember member)
        {
            try
            {
                if (member != null)
                {
                    await _membersService.CreateMember(member);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid id)
        {


            try
            {
               var result = await _membersService.DeleteMember(id);
               if (result)
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);
                
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            return StatusCode((int)HttpStatusCode.BadRequest,"No elem found");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] Guid id,[FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                
                var updatedMember = await _membersService.UpdateMember(id, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
           
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMember([FromRoute]Guid id, [FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedMember = await _membersService.UpdatePartiallyMember(id, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }
}
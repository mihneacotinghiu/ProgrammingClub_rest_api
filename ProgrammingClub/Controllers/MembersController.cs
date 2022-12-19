using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Net;
using System.Net.WebSockets;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [Route("GetMembers")]
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
         
            try
            {
                var members = await _membersService.GetMembers();
                if (members == null || !members.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(members);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [Route("GetMemberById")]
        [HttpGet]
        public async Task<IActionResult> GetMemberById([FromQuery] Guid id)
        {

            try
            {
                Member? member = await _membersService.GetMemberById(id);
                if (member != null)
                    return Ok(member);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [Route("PostMember")]
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

        [Route("DeleteMember")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMember([FromQuery] Guid id)
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

        [Route("PutMember")]
        [HttpPut]
        public async Task<IActionResult> PutMember(Guid idMember,[FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                
                var updatedMember = await _membersService.UpdateMember(idMember, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
           
        }

        [Route("PatchMember")]
        [HttpPatch]
        public async Task<IActionResult> PatchMember(Guid idMember, [FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedMember = await _membersService.UpdatePartiallyMember(idMember, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessegesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }
}
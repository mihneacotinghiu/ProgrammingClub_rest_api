using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System.Net;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembershipsController : Controller
    {
        private readonly IMembershipsService _membershipsService;

        public MembershipsController(IMembershipsService membershipsService)
        {
            _membershipsService = membershipsService;
        }

        [Route("GetMemberships")]
        [HttpGet]
        public async Task<IActionResult> GetMemberships()
        {


            try
            {
                DbSet<Membership> memberships = await _membershipsService.GetMembershipsAsync();
                if (memberships != null && memberships.ToList().Count > 0)
                    return Ok(memberships);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }


        }



        [Route("GetMembership")]
        [HttpGet]
        public async Task<IActionResult> GetMembership([FromQuery] Guid id)
        {

            try
            {
                var membership = await _membershipsService.GetMembershipByIdAsync(id);
                if (membership != null)
                {
                    return Ok(membership);

                }
                   
           
               
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);

        }


        [Route("CreateMembership")]
        [HttpPost]
        public async Task<IActionResult> CreateMembership([FromBody] Membership membership)
        {
            try
            {
                if (membership != null)
                {
                    //await _membershipsService.isValid(membership);
                    await _membershipsService.CreateMembershipAsync(membership);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [Route("DeleteMembership")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMembership([FromQuery] Guid id)
        {


            try
            {

                var result = await _membershipsService.DeleteMembershipAsync(id);
                if (result)
                {
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                   
                return StatusCode((int)HttpStatusCode.NotFound);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            //return StatusCode((int)HttpStatusCode.NotFound);
        }



        [Route("PutMembership")]
        [HttpPut]
        public async Task<IActionResult> UpdateMembership([FromBody] Membership membership)
        {

            try
            {
                if (membership != null)
                {
                    await _membershipsService.UpdateMembershipAsync(membership);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }
}

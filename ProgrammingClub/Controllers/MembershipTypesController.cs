using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System.Diagnostics.Metrics;
using System.Net;
using System.Runtime.InteropServices;

namespace ProgrammingClub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        private readonly IMembershipTypesService _membershipTypesService;
        public MembershipTypesController(IMembershipTypesService membershipTypesService)
        {
            _membershipTypesService = membershipTypesService;
        }

        [Route("GetMembershipTypesById")]
        [HttpGet]
        public  async Task<IActionResult> GetMembershipTypesAsync()
        {
            try
            {
                DbSet<MembershipType> membershipsTypes = await _membershipTypesService.GetMembershipTypesAsync();
                if (membershipsTypes != null && membershipsTypes.ToList().Count > 0)
                    return Ok(membershipsTypes);

                //return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [Route("GetMembershipTypes")]
        [HttpGet]
        public async Task<IActionResult> GetMembershipTypesByidAsync([FromQuery]Guid id)
        {
            try
            {
                var membershipsType = await _membershipTypesService.GetMemberByIdAsync(id);
                if (membershipsType != null)
                    return Ok(membershipsType);

               // return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            return StatusCode((int)HttpStatusCode.NoContent, ErrorMessegesEnum.NoElementFound);
        }


        [Route("CreateMembershipTypes")]
        [HttpPost]
        public async Task<IActionResult> CreateMembershipTypesAsync([FromBody]MembershipType membershipType)
        {
            try
            {
 
                if (membershipType != null)
                {

                    membershipType.IdMembershipType= Guid.NewGuid();
                    await _membershipTypesService.CreateMembershiptTypeAsync(membershipType);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [Route("DeleteMembershipType")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMembershipTypesAsync([FromBody] Guid IdmembershipType)
        {
            try
            {
                var result = await _membershipTypesService.DeleteMembershiptTypeAsync(IdmembershipType);
                if(result)
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
             
               // return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            //notfounderror
            return StatusCode((int)HttpStatusCode.NoContent,"not found");
        }


        [Route("PutMembershipType")]
        [HttpPut]
        public async Task<IActionResult> UpdateMembershipTypesAsync([FromBody] MembershipType membershipType)
        {
            try
            {
                if (membershipType != null)
                {
                    await _membershipTypesService.UpdateMembershiptTypeAsync(membershipType);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }




    }
    


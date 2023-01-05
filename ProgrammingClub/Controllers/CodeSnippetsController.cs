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
    public class CodeSnippetsController : Controller
    {
        private readonly ICodeSnippetsService _codeSnippetsService;

        public CodeSnippetsController(ICodeSnippetsService codeSnippetsService)
        {
            _codeSnippetsService = codeSnippetsService;
        }

        [Route("GetCodeSnippets")]
        [HttpGet]
        public async Task<IActionResult> GetCodeSnippets()
        {
          
            try
            {
                DbSet<CodeSnippet> codeSnippets = await _codeSnippetsService.GetCodeSnippetAsync();
                if (codeSnippets != null && codeSnippets.ToList().Count > 0)
                    return Ok(codeSnippets);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [Route("GetCodeSnippet")]
        [HttpGet]
        public async Task<IActionResult> GetCodeSnippet([FromQuery]Guid id)
        {

            try
            {
                CodeSnippet? codeSnippet = await _codeSnippetsService.GetCodeSnippetByid(id);
                if (codeSnippet == null)
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                return Ok(codeSnippet);
               
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [Route("CreateCodeSnippets")]
        [HttpPost]
        public async Task<IActionResult> CreateCodeSnippet([FromBody] CodeSnippet codeSnippet)
        {

            try
            {
                if(codeSnippet != null) {
                    await _codeSnippetsService.CreateCodeSnippetAsync(codeSnippet);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }

                return StatusCode((int)HttpStatusCode.BadRequest);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }

    
        }

        [Route("DeleteCodeSnippets")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCodeSnippet([FromQuery] Guid id)
        {

            try
            {
                var result = await _codeSnippetsService.DeleteCodeSnippetAsync(id);
                if (result)
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyDeleted);
               
                return StatusCode((int)HttpStatusCode.BadRequest);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }


         
        }
        [Route("PutCodeSnippets")]
        [HttpPut]
        public async Task<IActionResult> UpdateCodeSnippet([FromBody] CodeSnippet codeSnippet)
        {

            try
            {
                if (codeSnippet != null)
                {
                    await _codeSnippetsService.UpdateCodeSnippetAsync(codeSnippet);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }
    }
}

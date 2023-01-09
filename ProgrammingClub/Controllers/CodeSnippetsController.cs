using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Exceptions;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Services;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSnippetsController : Controller
    {
        private readonly ICodeSnippetsService _codeSnippetsService;


        public CodeSnippetsController(ICodeSnippetsService codeSnippetsService)
        {
            _codeSnippetsService = codeSnippetsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCodeSnippets()
        {
            try
            {
                var codeSnippets = await _codeSnippetsService.GetCodeSnippetsAsync();
                if(codeSnippets == null || !codeSnippets.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(codeSnippets);

            }
            catch (Exception ex) {return StatusCode((int)HttpStatusCode.InternalServerError, ex);}
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeSnippet([FromRoute]Guid id)
        {
            try
            {
                var codeSnippet = await _codeSnippetsService.GetCodeSnippetByIdAsync(id);
                if(codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(codeSnippet);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpPost]
        public async Task<IActionResult> PostCodeSnippet([FromBody]CreateCodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                await _codeSnippetsService.CreateCodeSnippetAsync(codeSnippet);
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);

            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeSnippet([FromRoute]Guid id)
        {
            try
            {
                var result = await _codeSnippetsService.DeleteCodeSnippetAsync(id);
                if (result)
                {
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                return StatusCode((int)HttpStatusCode.BadRequest,Helpers.ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeSnippet([FromRoute]Guid id , [FromBody]CodeSnippet codeSnippet)
        {
            try
            {  
                if(codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedCodeSnippet = await _codeSnippetsService.UpdateCodeSnippetAsync(id, codeSnippet);

                if(updatedCodeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);

            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCodeSnippet([FromRoute] Guid id, [FromBody] CodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                var updatedPartyallyCodeSnippet = await _codeSnippetsService.PartiallyUpdateCodeSnippetAsync(id, codeSnippet);

                if (updatedPartyallyCodeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);

            }
            catch (ModelValidationException ex) { return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

    }

}


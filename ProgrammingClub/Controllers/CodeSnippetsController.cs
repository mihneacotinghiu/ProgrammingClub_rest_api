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
        private readonly ILogger<CodeSnippetsController> _logger;

        public CodeSnippetsController(
            ICodeSnippetsService codeSnippetsService,
            ILogger<CodeSnippetsController> logger
            )
        {
            _codeSnippetsService = codeSnippetsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCodeSnippets()
        {
            try
            {
                _logger.LogInformation("GetCodeSnippets start");
                var codeSnippets = await _codeSnippetsService.GetCodeSnippetsAsync();
                _logger.LogInformation($"GetCodeSnippets end,total result: {codeSnippets.Count()}");
                if (codeSnippets == null || !codeSnippets.Any())
                {
                    _logger.LogInformation("GetCodeSnippets no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(codeSnippets);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"GetCodeSnippets error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeSnippet([FromRoute]Guid id)
        {
            try
            {
                _logger.LogInformation($"GetCodeSnippet start,id: {id}");
                var codeSnippet = await _codeSnippetsService.GetCodeSnippetByIdAsync(id);
                _logger.LogInformation("GetCodeSnippet end");
                if (codeSnippet == null)
                {
                    _logger.LogInformation("GetCodeSnippet no element found");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(codeSnippet);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"GetCodeSnippet error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCodeSnippet([FromBody]CreateCodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    _logger.LogInformation("PostCodeSnippet FromBody codeSnippet is null");

                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                _logger.LogInformation($"PostCodeSnippet start , {codeSnippet.GetLoggingInfo()}");
                await _codeSnippetsService.CreateCodeSnippetAsync(codeSnippet);
                _logger.LogInformation("PostCodeSnippet end");
                return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogInformation($"PostCodeSnippet validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PostCodeSnippet error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeSnippet([FromRoute]Guid id)
        {
            try
            {
                _logger.LogInformation($"DeleteCodeSnippet start , id: {id}");
                var result = await _codeSnippetsService.DeleteCodeSnippetAsync(id);
                _logger.LogInformation("DeleteCodeSnippet end");
                if (result)
                {
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);
                }
                _logger.LogInformation("DeleteCodeSnippet no elem found");
                return StatusCode((int)HttpStatusCode.BadRequest,Helpers.ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteCodeSnippet error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeSnippet([FromRoute]Guid id , [FromBody]CreateCodeSnippet codeSnippet)
        {
            try
            {  
                if(codeSnippet == null)
                {
                    _logger.LogInformation("PutCodeSnippet FromBody codeSnippet is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                codeSnippet.IdCodeSnippet = id;
                _logger.LogInformation($"PutCodeSnippet starts {codeSnippet.GetLoggingInfo()}");
                var updatedCodeSnippet = await _codeSnippetsService.UpdateCodeSnippetAsync(id, codeSnippet);
                _logger.LogInformation("PutCodeSnippet ends");
                if (updatedCodeSnippet == null)
                {
                    _logger.LogInformation("PutCodeSnippet no element found");
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogInformation($"PutCodeSnippet validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutCodeSnippet error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCodeSnippet([FromRoute] Guid id, [FromBody] CodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    _logger.LogInformation("PatchCodeSnippet FromBody codeSnippet is null");
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                codeSnippet.IdCodeSnippet = id;
                _logger.LogInformation($"PatchCodeSnippet starts,codeSnippet: {codeSnippet.GetLoggingInfo()}");
                var updatedPartyallyCodeSnippet = await _codeSnippetsService.PartiallyUpdateCodeSnippetAsync(id, codeSnippet);
                _logger.LogInformation($"PatchCodeSnippet end");
                if (updatedPartyallyCodeSnippet == null)
                {
                    _logger.LogInformation("PatchCodeSnippet no element found");
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex) 
            {
                _logger.LogInformation($"PatchCodeSnippet validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (Exception ex) 
            {
                _logger.LogError($"PatchCodeSnippet error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }

}


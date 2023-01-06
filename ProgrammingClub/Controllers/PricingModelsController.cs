using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System.Net;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingModelsController : Controller
    {
        private readonly IPricingModelsService _pricingModelsService;
        public PricingModelsController(IPricingModelsService pricingModels)
        {
            _pricingModelsService = pricingModels;
        }

        [HttpGet]
        public async Task<IActionResult> GetPricingModels()
        {
            try
            {
                var pricingModels = await _pricingModelsService.GetPricingModelsAsync();
                if (pricingModels != null && pricingModels.ToList().Count > 0)
                    return Ok(pricingModels);

                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPricingModelsById(Guid id)
        {
            try
            {
                PricingModels? pricingModels = await _pricingModelsService.GetPricingModelByIdAsync(id);

                if (pricingModels != null)

                    return Ok(pricingModels);



                return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);

            }

            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
        }

        [HttpPost]
        public async Task<IActionResult> PostPricingModels([FromBody] PricingModels pricingModels)
        {
            try
            {
                if (pricingModels != null)
                {
                    await _pricingModelsService.CreatePricingModelsAsync(pricingModels);
                    return Ok(SuccessMessegesEnum.ElementSuccesfullyAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMember([FromQuery] Guid id)
        {
            try
            {
                var result = await _pricingModelsService.DeletePricingModelsAsync(id);
                if (result)
                    return Ok(Helpers.SuccessMessegesEnum.ElementSuccesfullyDeleted);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }
            return StatusCode((int)HttpStatusCode.BadRequest, "No element found");

        }


        [HttpPut]
        public async Task<IActionResult> PutMember(Guid idPricingModels, [FromBody] PricingModels pricingModels)
        {
            try
            {
                if (pricingModels == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedPricingModels = await _pricingModelsService.UpdatePricingModelsAsync(idPricingModels, pricingModels);
                if (updatedPricingModels == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }

        [HttpPatch]
        public async Task<IActionResult> PatchEventType(Guid idPricingModels, [FromBody] PricingModels pricingModels)
        {
            try
            {
                if (pricingModels == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                var updatedPricingModels= await _pricingModelsService.UpdatePricingModelsPartiallyAsync(idPricingModels, pricingModels);
                if (updatedPricingModels == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoElementFound);
                }

                return Ok(SuccessMessegesEnum.ElementSuccesfullyUpdated);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex); }

        }

    }
}

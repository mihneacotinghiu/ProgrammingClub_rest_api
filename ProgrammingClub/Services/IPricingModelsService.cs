using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IPricingModelsService
    {

        public Task<IEnumerable<PricingModel>> GetPricingModelsAsync();
        public Task CreatePricingModelsAsync(PricingModel pricingModels);
        public Task <PricingModel?>UpdatePricingModelsAsync(Guid id, PricingModel pricingModels);
        public Task<bool> DeletePricingModelsAsync(Guid id);
        public Task<PricingModel?> GetPricingModelByIdAsync(Guid id);
        public Task<PricingModel?> UpdatePricingModelsPartiallyAsync(Guid id, PricingModel pricingModels);
        public Task<bool> PricingModelExistByIdAsync(Guid id);
    }
}

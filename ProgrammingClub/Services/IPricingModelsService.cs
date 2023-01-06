using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IPricingModelsService
    {

        public Task<IEnumerable<PricingModels>> GetPricingModelsAsync();
        public Task CreatePricingModelsAsync(PricingModels pricingModels);
        public Task <PricingModels?>UpdatePricingModelsAsync(Guid id, PricingModels pricingModels);
        public Task<bool> DeletePricingModelsAsync(Guid id);
        public Task<PricingModels?> GetPricingModelByIdAsync(Guid id);
        public Task<PricingModels?> UpdatePricingModelsPartiallyAsync(Guid id, PricingModels pricingModels);
        public Task<bool> PricingModelExistByIdAsync(Guid id);
    }
}

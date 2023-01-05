using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IPricingModelsService
    {

        public Task<DbSet<PricingModels>> GetPricingModelsAsync();
        public Task CreatePricingModelsAsync(PricingModels pricingModels);
        public Task <PricingModels?>UpdatePricingModelsAsync(Guid id, PricingModels pricingModels);
        public Task<bool> DeletePricingModelsAsync(Guid id);
        public Task<PricingModels?> GetPricingModelsByIdAsync(Guid id);
        public Task<PricingModels?> UpdatePricingModelsPartiallyAsync(Guid id, PricingModels pricingModels);
    }
}

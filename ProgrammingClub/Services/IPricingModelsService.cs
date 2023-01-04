using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface IPricingModelsService
    {

        public Task<DbSet<PricingModels>> GetPricingModelsAsync();
        public Task CreatePricingModelsAsync(PricingModels pricingModels);
        public Task UpdatePricingModelsAsync(PricingModels pricingModels);
        public Task<bool> DeletePricingModelsAsync(Guid id);
        public Task<EventType?> GetPricingModelsByIdAsync(Guid id);

    }
}

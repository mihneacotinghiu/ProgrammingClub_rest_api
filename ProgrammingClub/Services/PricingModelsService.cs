using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class PricingModelsService : IPricingModelsService
    {
        private readonly ProgrammingClubDataContext _context;
        public Task CreatePricingModelsAsync(PricingModels pricingModels)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePricingModelsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DbSet<PricingModels>> GetPricingModelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EventType?> GetPricingModelsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePricingModelsAsync(PricingModels pricingModels)
        {
            throw new NotImplementedException();
        }
    }
}

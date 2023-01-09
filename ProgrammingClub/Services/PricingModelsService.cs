using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class PricingModelsService : IPricingModelsService
    {
        private readonly ProgrammingClubDataContext _context;

        public PricingModelsService(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public async Task CreatePricingModelsAsync(PricingModel pricingModels)
        {
            if (pricingModels == null)
                throw new Exception();

            pricingModels.IdPricingModel = Guid.NewGuid();


            _context.Entry(pricingModels).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePricingModelsAsync(Guid id)
        {
            PricingModel? pricingModels = await GetPricingModelByIdAsync(id);
            if (pricingModels == null)
                return false;
            _context.PricingModels.Remove(pricingModels);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PricingModel>> GetPricingModelsAsync()
        {
            return await _context.PricingModels.ToListAsync();
        }

        public async  Task<PricingModel?> GetPricingModelByIdAsync(Guid id)
        {
            return await _context.PricingModels.FirstOrDefaultAsync(p => p.IdPricingModel == id);
        }


        public async Task<PricingModel?> UpdatePricingModelsAsync(Guid id, PricingModel pricingModels)
        {
            if (GetPricingModelByIdAsync(id) == null)
                return null;
            _context.Update(pricingModels);
            await _context.SaveChangesAsync();
            return pricingModels;
        }

        public async Task<PricingModel?> UpdatePricingModelsPartiallyAsync(Guid id, PricingModel pricingModels)
        {
            var pricingModelsFromDatabase = await GetPricingModelByIdAsync(id);

            if (pricingModelsFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(pricingModels.Name))
            {
                pricingModelsFromDatabase.Name = pricingModels.Name;
            }
            if (pricingModels.Price >= 0 )
            {
                pricingModelsFromDatabase.Price = pricingModels.Price;
            }

            if (pricingModels.ModifiedDate != null)
            {
                pricingModelsFromDatabase.ModifiedDate = pricingModels.ModifiedDate;
            }


            _context.Update(pricingModels);
            await _context.SaveChangesAsync();
            return pricingModelsFromDatabase;
        }

        public async Task<bool> PricingModelExistByIdAsync(Guid id)
        {
            return await _context.PricingModels.AnyAsync(p => p.IdPricingModel == id);
        }
    }
}

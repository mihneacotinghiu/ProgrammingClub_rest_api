﻿using Microsoft.EntityFrameworkCore;
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

        public async Task CreatePricingModelsAsync(PricingModels pricingModels)
        {
            if (pricingModels == null)
                throw new Exception();

            pricingModels.IdPricingModels = Guid.NewGuid();


            _context.Entry(pricingModels).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePricingModelsAsync(Guid id)
        {
            PricingModels? pricingModels = await GetPricingModelByIdAsync(id);
            if (pricingModels == null)
                return false;
            _context.PricingModels.Remove(pricingModels);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PricingModels>> GetPricingModelsAsync()
        {
            return await _context.PricingModels.ToListAsync();
        }

        public async  Task<PricingModels?> GetPricingModelByIdAsync(Guid id)
        {
            return await _context.PricingModels.FirstOrDefaultAsync(p => p.IdPricingModels == id);
        }


        public async Task<PricingModels?> UpdatePricingModelsAsync(Guid id, PricingModels pricingModels)
        {
            if (GetPricingModelByIdAsync(id) == null)
                return null;
            _context.Update(pricingModels);
            await _context.SaveChangesAsync();
            return pricingModels;
        }

        public async Task<PricingModels?> UpdatePricingModelsPartiallyAsync(Guid id, PricingModels pricingModels)
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
            return await _context.PricingModels.AnyAsync(p => p.IdPricingModels == id);
        }
    }
}

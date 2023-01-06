using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using System.Data;

namespace ProgrammingClub.Services
{
    public class EventTypeService : IEventTypeService
       
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IPricingModelsService _pricingModelsService;
        private readonly IMapper _mapper;

        public EventTypeService(
            ProgrammingClubDataContext context,
            IPricingModelsService pricingModelsService,
            IMapper mapper)
        {
            _context = context;
            _pricingModelsService = pricingModelsService;
            _mapper = mapper;
        }

        public async Task CreateEventTypeAsync(CreateEventType eventType)
        {
            if (eventType == null)
                throw new Exception();
            if (!await _pricingModelsService.PricingModelExistByIdAsync(eventType.IdPricingModel))
            {
                throw new Exception("No PriceingModel exists!");
            }

            var newEventType = _mapper.Map<EventType>(eventType);
            newEventType.IdEventType = Guid.NewGuid();
            _context.Entry(newEventType).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEventTypeAsync(Guid id)
        {
            EventType? eventType = await GetEventTypeByIdAsync(id);
            if (eventType == null)
                return false;
            _context.EventType.Remove(eventType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventType>> GetEventTypesAsync()
        {
            return await _context.EventType.ToListAsync();
        }

        public async Task<EventType?> GetEventTypeByIdAsync(Guid id)
        {
            return await _context.EventType.FirstOrDefaultAsync(e => e.IdEventType == id);
        }

        public async Task<EventType?> UpdateEventTypeAsync(Guid id, EventType eventType)
        {
            if (!await EventTypeExistsByIdAsync(id))
                return null;
            if (!eventType.IdPricingModel.HasValue || !await _pricingModelsService.PricingModelExistByIdAsync(eventType.IdPricingModel.Value))
            {
                throw new Exception("No PriceingModel exists!");
            }

            eventType.IdEventType = id;
            _context.Update(eventType);
            await _context.SaveChangesAsync();
            return eventType;
        }

        public async Task<EventType?> UpdateEventTypePartiallyAsync(Guid id, EventType eventType)
        {
            var eventTypeFromDatabase = await GetEventTypeByIdAsync(id);
            if (eventTypeFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(eventType.Name))
            {
                eventTypeFromDatabase.Name = eventType.Name;
            }
            if (eventType.Description != null)
            {
                eventTypeFromDatabase.Description = eventType.Description;
            }
            
            if (eventType.IdPricingModel.HasValue && eventTypeFromDatabase.IdPricingModel != eventType.IdPricingModel)
            {
                if (!await _pricingModelsService.PricingModelExistByIdAsync(eventType.IdPricingModel.Value))
                {
                    throw new Exception("No PriceingModel exists!");
                }
                eventTypeFromDatabase.IdPricingModel = eventType.IdPricingModel;
            }

            _context.Update(eventTypeFromDatabase);
            await _context.SaveChangesAsync();
            return eventTypeFromDatabase;
        }

        public async Task<bool> EventTypeExistsByIdAsync(Guid id)
        {
            return await _context.EventType.AnyAsync(e => e.IdEventType == id);
        }
    }
}

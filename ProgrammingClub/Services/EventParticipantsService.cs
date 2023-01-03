﻿using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using System.Linq;

namespace ProgrammingClub.Services
{
    public class EventParticipantsService:IEventParticipantsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;

        public EventParticipantsService(ProgrammingClubDataContext context, IMembersService membersService)
        {
            _context = context;
            _membersService = membersService;
        }

        public async Task<IEnumerable<EventParticipant>> GetAllMemberParticipations(Guid idMember)
        {
            //returns list of all EventParticipant entries that have the given idMember
            var participants = _context.EventParticipants.Where(e => e.IdMember == idMember);
            return await participants.ToListAsync();
        }

        /* UNCOMMENT WHEN EVENTS ARE DONE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
         * public async Task<IEnumerable<EventParticipant>> GetAllMemberParticipations(Guid idEvent)
        {
            //returns all Event Participants entries that have the given idMember
            var participants = _context.EventParticipants.Where(e => e.IdEvent == idEvent);
            return participants.ToList();
        }
         */

        public async Task CreateEventParticipant(CreateEventParticipant participant)
        {
            //add changes for event
            participant.IdEventParticipant = Guid.NewGuid();
            
            //verifying that a member with said ID exists
            if (!await _membersService.MemberExistByIdAsync(participant.IdMember))
                throw new Exception();


            //Check that a member does not already have a listing to an event. ONE TO ONE RELATIONSHIP.
            //Doesn't need to be done for event as doing it for member ensures it is safe - MAYBE????
            List<EventParticipant> participants = (List<EventParticipant>)await GetAllMemberParticipations(participant.IdMember);
            for (int i = 0; i< participants.Count; i++)
            {
                if (participants[i].IdEvent == participant.IdEvent)
                {
                    throw new Exception();
                }
            }

            participant.Paid = false;
            participant.Present = false;
            _context.Entry(participant).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEventParticipant(Guid idEventParticipant)
        {
            if (!await EventParticipantExists(idEventParticipant))
                return false;
            
            _context.EventParticipants.Remove(new EventParticipant { IdEventParticipant = idEventParticipant });
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<EventParticipant>> GetEventsParticipantsAsync()
        {
            return _context.EventParticipants.ToList();
        }

        public async Task<EventParticipant?> UpdateEventParticipant(Guid idEventParticipant, EventParticipant eventParticipant)
        {
            if (!await EventParticipantExists(idEventParticipant))
            {
                return null;
            }

            eventParticipant.IdEventParticipant = idEventParticipant;
            List<EventParticipant> participants = (List<EventParticipant>)await GetAllMemberParticipations((Guid)eventParticipant.IdMember);
            for (int i = 0; i < participants.Count; i++)
            {
                if (participants[i].IdEvent == eventParticipant.IdEvent)
                {
                    return null;
                }
            }


            _context.Update(eventParticipant);
            _context.SaveChanges();
            return eventParticipant;
        }

        public async Task<EventParticipant?> UpdateEventParticipantPartially(Guid idEventParticipant, EventParticipant participant)
        {
            var eventParticipantFromDb = await GetEventParticipantById(idEventParticipant);
            if (eventParticipantFromDb == null)
            {
                return null;
            }

            if (participant.IdEvent != null)
            {
                eventParticipantFromDb.IdEvent = participant.IdEvent; 
            }

            if (participant.IdMember != null)
            {
                List<EventParticipant> participants = (List<EventParticipant>)await GetAllMemberParticipations((Guid)participant.IdMember);
                for (int i = 0; i < participants.Count; i++)
                {
                    if (participants[i].IdEvent == participant.IdEvent)
                    {
                        //does not change the member if the newly given member id already has a participation booked for the event
                        return null;
                    }
                }
                eventParticipantFromDb.IdMember = participant.IdMember;
            }

            // paid and present to be handled by other endpoints

            _context.Update(eventParticipantFromDb);
            await _context.SaveChangesAsync();
            return eventParticipantFromDb;
        }

        public async Task<bool> UpdateEventParticipantPaid(Guid id)
        {
            //endpoint that sets the paid value as true, separate from regular update as it shouldn't be up to the user to mark as true
            var eventParticipantFromDb = await GetEventParticipantById(id);
            if (eventParticipantFromDb == null)
            {
                return false;
            }

            eventParticipantFromDb.Paid = true;
            _context.Update(eventParticipantFromDb);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateEventParticipantPresent(Guid id)
        {
            var eventParticipantFromDb = await GetEventParticipantById(id);
            if(eventParticipantFromDb == null)
            {
                return false;
            }

            eventParticipantFromDb.Present = true;
            _context.Update(eventParticipantFromDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EventParticipant?> GetEventParticipantById(Guid id)
        {
            return await _context.EventParticipants.FirstOrDefaultAsync(e => e.IdEventParticipant == id);
        }

        public async Task<bool> EventParticipantExists(Guid id)
        {
            return await _context.EventParticipants.CountAsync() > 0;
        }
    }
}

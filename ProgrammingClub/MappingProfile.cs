﻿using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace CustomerPortal.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, CreateMember>().ReverseMap();
            CreateMap<EventType, CreateEventType>().ReverseMap();
            CreateMap<EventsParticipant, CreateEventsParticipant>().ReverseMap();
            CreateMap<EventType, CreateEventType>().ReverseMap();
        }
    }
}

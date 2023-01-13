using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;

namespace CustomerPortal.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, CreateMember>().ReverseMap();
            CreateMap<Moderator, CreateModerator>().ReverseMap();
            CreateMap<Event, CreateEvent>().ReverseMap();
            CreateMap<EventType, CreateEventType>().ReverseMap();
            CreateMap<CodeSnippet, CreateCodeSnippet>().ReverseMap();
            CreateMap<Announcement, CreateAnnouncement>().ReverseMap();
            CreateMap<Dropout, CreateDropout>().ReverseMap();
            CreateMap<EventsParticipant, CreateEventsParticipant>().ReverseMap();
        }
    }
}

using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Models.CreateModerator;

namespace CustomerPortal.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, CreateMember>().ReverseMap();
            CreateMap<Moderator, CreateModerator>().ReverseMap();
            CreateMap<Event, CreateEvent>().ReverseMap();
        }
    }
}

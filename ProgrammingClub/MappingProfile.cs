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
        }
    }
}

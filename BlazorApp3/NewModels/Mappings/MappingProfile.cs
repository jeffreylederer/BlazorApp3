using AutoMapper;
using BlazorApp3.Model;
using BlazorApp3.NewModels.DTOs;

namespace BlazerApp1.Models.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO,User>().ReverseMap();
            CreateMap<LeagueDTO,League>().ReverseMap();
            CreateMap<League, LeagueDTO>().ReverseMap();
            CreateMap<Membership, MembershipDTO>().ReverseMap();
            CreateMap<Schedule, ScheduleDTO>().ReverseMap();
        }
    }
}

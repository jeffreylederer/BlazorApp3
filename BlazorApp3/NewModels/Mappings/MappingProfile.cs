using AutoMapper;
using BlazerApp3.Models.DTOs;
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
        }
    }
}

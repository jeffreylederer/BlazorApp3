using AutoMapper;
using BlazerApp1.Models.DTOs;
using BlazorApp3.Model;

namespace BlazorApp3.Models.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO,User>().ReverseMap();
        }
    }
}

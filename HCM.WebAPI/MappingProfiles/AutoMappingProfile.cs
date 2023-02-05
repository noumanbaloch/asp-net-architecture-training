using AutoMapper;
using HCM.Models.Dtos.User.Request;
using HCM.Models.Entities;

namespace HCM.WebAPI.MappingProfiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile() 
        {
            CreateMap<RegisterUserRequestDto, UserEntity>();
        }
    }
}

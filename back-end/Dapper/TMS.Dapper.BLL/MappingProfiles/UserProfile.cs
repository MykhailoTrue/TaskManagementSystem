using AutoMapper;
using TMS.Dapper.Common.DTOs.Users.CRUD;
using TMS.Dapper.Common.DTOs.Users.Custom;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserWithProjectsDTO>().ReverseMap();
        }
    }
}

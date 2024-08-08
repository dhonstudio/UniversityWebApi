using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UsersParamDTO, Users>();
            CreateMap<Users, UsersDTO>();
            CreateMap<Users, UsersParamDTO>();

            CreateMap<UserRoleParamDTO, UserRoles>();
            CreateMap<UserRoles, UserRolesDTO>();
        }
    }
}

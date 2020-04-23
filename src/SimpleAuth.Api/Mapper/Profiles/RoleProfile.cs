using AutoMapper;
using SimpleAuth.Api.Models;
using SimpleAuth.Common.Entities;

namespace SimpleAuth.Api.Mapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleVM>();
        }
    }
}

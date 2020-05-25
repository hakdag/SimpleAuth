using AutoMapper;
using SimpleAuth.Api.Models;
using SimpleAuth.Common.Entities;

namespace SimpleAuth.Api.Mapper.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionVM>();
        }
    }
}

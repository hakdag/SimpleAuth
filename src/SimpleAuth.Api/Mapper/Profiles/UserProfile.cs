using AutoMapper;
using SimpleAuth.Api.Models;
using SimpleAuth.Common.Entities;
using System.Linq;

namespace SimpleAuth.Api.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVM>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToArray()));
        }
    }
}

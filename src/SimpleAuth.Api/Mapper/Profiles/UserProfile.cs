using AutoMapper;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;

namespace SimpleAuth.Api.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVM>();
        }
    }
}

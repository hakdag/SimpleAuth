using SimpleAuth.Common;
using SimpleAuth.Common.Entities;

namespace SimpleAuth.Contracts.Business
{
    public interface ITokenGenerationBusiness
    {
        AuthenticationToken Generate(User user, string secret);
    }
}

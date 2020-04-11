using SimpleAuth.Common;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthenticationService
    {
        AuthenticationToken Authenticate(string username, string password);
    }
}

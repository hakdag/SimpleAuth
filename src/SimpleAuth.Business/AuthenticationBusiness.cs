using Microsoft.Extensions.Options;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.Strategies;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly AppSettings appSettings;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserBusiness userBusiness;
        private readonly ILockAccountBusiness lockAccountBusiness;
        private readonly IAuthenticateAttemptBusiness authenticateAttemptBusiness;
        private readonly IAuthenticateAttempsStrategy authenticateAttempsStrategy;
        private readonly ITokenGenerationBusiness tokenGenerationBusiness;

        public AuthenticationBusiness(
            IOptions<AppSettings> appSettings,
            IPasswordHasher passwordHasher,
            IUserBusiness userBusiness,
            ILockAccountBusiness lockAccountBusiness,
            IAuthenticateAttemptBusiness authenticateAttemptBusiness,
            IAuthenticateAttempsStrategy authenticateAttempsStrategy,
            ITokenGenerationBusiness tokenGenerationBusiness)
        {
            this.appSettings = appSettings.Value;
            this.passwordHasher = passwordHasher;
            this.userBusiness = userBusiness;
            this.lockAccountBusiness = lockAccountBusiness;
            this.authenticateAttemptBusiness = authenticateAttemptBusiness;
            this.authenticateAttempsStrategy = authenticateAttempsStrategy;
            this.tokenGenerationBusiness = tokenGenerationBusiness;
        }

        public async Task<AuthenticationToken> Authenticate(string username, string password)
        {
            var user = await userBusiness.GetByUserName(username);
            if (user == null)
            {
                return null;
            }

            var lockCheck = await lockAccountBusiness.CheckUser(user.Id);
            if (!lockCheck.Success)
            {
                return new AuthenticationToken { islocked = true, message = lockCheck.Messages[0] };
            }

            var attemptResult = await authenticateAttempsStrategy.Check(user.Id, user.Password, password);
            if (!attemptResult.Verified)
            {
                return attemptResult.UnverifiedAttempt;
            }

            // authentication successful so generate jwt token
            var authenticationToken = tokenGenerationBusiness.Generate(user, appSettings.Secret);

            // update user login date and token fields
            await userBusiness.UserLoggedIn(user, authenticationToken.token);

            return authenticationToken;
        }

        public async Task<ResponseResult> Logout(string token)
        {
            var logoutResponse = await userBusiness.UserLoggedOut(token);
            return logoutResponse;
        }
    }
}

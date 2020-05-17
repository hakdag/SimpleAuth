using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.Strategies;
using System.Threading.Tasks;

namespace SimpleAuth.Business.Strategies
{
    public class AuthenticateWithUnlimitedAttempsStrategy : IAuthenticateAttempsStrategy
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IAuthenticateAttemptBusiness authenticateAttemptBusiness;

        public AuthenticateWithUnlimitedAttempsStrategy(
            IPasswordHasher passwordHasher,
            IAuthenticateAttemptBusiness authenticateAttemptBusiness)
        {
            this.passwordHasher = passwordHasher;
            this.authenticateAttemptBusiness = authenticateAttemptBusiness;
        }

        public async Task<AuthenticateAttempResult> Check(long userId, string hash, string password)
        {
            var checkResult = passwordHasher.Check(hash, password);
            return new AuthenticateAttempResult { Verified = checkResult.Verified };
        }
    }
}

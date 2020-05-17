using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.Strategies;
using System.Threading.Tasks;

namespace SimpleAuth.Business.Strategies
{
    public class AuthenticateWithRemainingAttempsStrategy : IAuthenticateAttempsStrategy
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IAuthenticateAttemptBusiness authenticateAttemptBusiness;
        private readonly ILockAccountBusiness lockAccountBusiness;

        public readonly string WarningMessage_AuthenticateFailed = "Authenticate was failed.";
        public readonly string WarningMessage_AccountIsLocked = "Account is locked.";

        public AuthenticateWithRemainingAttempsStrategy(
            IPasswordHasher passwordHasher,
            IAuthenticateAttemptBusiness authenticateAttemptBusiness,
            ILockAccountBusiness lockAccountBusiness)
        {
            this.passwordHasher = passwordHasher;
            this.authenticateAttemptBusiness = authenticateAttemptBusiness;
            this.lockAccountBusiness = lockAccountBusiness;
        }

        public async Task<AuthenticateAttempResult> Check(long userId, string hash, string password)
        {
            var checkResult = passwordHasher.Check(hash, password);
            if (checkResult.Verified)
            {
                var checkUserResponse = await lockAccountBusiness.CheckUser(userId);
                if (!checkUserResponse.Success) // account is locked
                {
                    return new AuthenticateAttempResult { Verified = false, UnverifiedAttempt = new AuthenticationToken { islocked = true, message = WarningMessage_AccountIsLocked } };
                }
                await authenticateAttemptBusiness.ResetRemainingAttempts(userId);
                return new AuthenticateAttempResult { Verified = true };
            }
            else
            {
                await authenticateAttemptBusiness.DecreaseRemainingAttempts(userId);
                var remainingAttempts = await authenticateAttemptBusiness.GetRemainingAttemptCount(userId);
                if (remainingAttempts < 0) // lock the account
                {
                    await lockAccountBusiness.LockAccount(userId);
                    return new AuthenticateAttempResult { Verified = false, UnverifiedAttempt = new AuthenticationToken { islocked = true, message = WarningMessage_AccountIsLocked, remainingattempts = remainingAttempts } };
                }

                return new AuthenticateAttempResult { Verified = false, UnverifiedAttempt = new AuthenticationToken { message = WarningMessage_AuthenticateFailed, islocked = false, remainingattempts = remainingAttempts } };
            }
        }
    }
}

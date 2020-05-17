using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using SimpleAuth.Contracts.Data.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Data.PasswordReset
{
    public class GeneratePasswordResetKeyData : BaseData<PasswordResetKey>, IGeneratePasswordResetKeyData
    {
        public readonly string ErrorMessage_PasswordResetKeyGenericError = "Error occured when storing the password reset key.";

        public GeneratePasswordResetKeyData(IRepository repository) : base(repository) { }

        public async Task<PasswordResetKeyResponse> StoreKey(long userId, string resetKey)
        {
            var result = await Execute("INSERT INTO public.passwordresetkey(userid,resetkey) VALUES(@userId, @resetKey)", new { userId, resetKey });
            if (result > 0)
            {
                return new PasswordResetKeyResponse { Success = true, ResetKey = resetKey };
            }

            return new PasswordResetKeyResponse { Success = false, Messages = new[] { ErrorMessage_PasswordResetKeyGenericError }, ResetKey = resetKey };
        }
    }
}

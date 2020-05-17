using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using SimpleAuth.Contracts.Data.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Data.PasswordReset
{
    public class PasswordResetData : BaseData<PasswordResetKey>, IPasswordResetData
    {
        public readonly string ErrorMessage_PasswordResetKeyGenericError = "Error occured when storing the password reset key.";
        public readonly string ErrorMessage_RemoveKeyError = "Error occured when removing the reset key.";

        public PasswordResetData(IRepository repository) : base(repository) { }

        public async Task<PasswordResetKey> Get(long userId, string resetKey)
        {
            var passwordResetKey = await RunQueryFirst("select userid, resetkey, createddate from public.passwordresetkey where userid = @userId and resetkey = @resetKey order by createddate desc limit 1", new { userId, resetKey });
            return passwordResetKey;
        }

        public async Task<ResponseResult> RemoveKey(long userId)
        {
            var result = await Execute("DELETE FROM public.passwordresetkey where userid = @userId", new { userId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RemoveKeyError } };
        }

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

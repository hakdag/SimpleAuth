using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.PasswordReset;
using SimpleAuth.Contracts.Data.PasswordReset;
using System;
using System.Threading.Tasks;

namespace SimpleAuth.Business.PasswordReset
{
    public class GeneratePasswordResetKeyBusiness : IGeneratePasswordResetKeyBusiness
    {
        private readonly IGeneratePasswordResetKeyData data;
        private readonly IUserBusiness userBusiness;

        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";

        public GeneratePasswordResetKeyBusiness(IGeneratePasswordResetKeyData data, IUserBusiness userBusiness)
        {
            this.data = data;
            this.userBusiness = userBusiness;
        }

        public async Task<PasswordResetKeyResponse> Generate(string userName)
        {
            // check if username exists
            var user = await userBusiness.GetByUserName(userName);
            if (user == null)
            {
                return new PasswordResetKeyResponse { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            // generate key
            var resetKey = Guid.NewGuid().ToString().GetHashCode().ToString("x");

            // store key
            var response = await data.StoreKey(user.Id, resetKey);

            // return the key
            return response;
        }
    }
}

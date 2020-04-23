using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class AssignToRoleData : BaseData<UserRole>, IAssignToRoleData
    {
        public readonly string ErrorMessage_GenericError = "Error occured when assinging role to the user.";

        public AssignToRoleData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Create(int userId, int roleId)
        {
            var result = await Execute("INSERT INTO public.\"userrole\"(userid,roleid) VALUES(@userId, @roleId)", new { userId, roleId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_GenericError } };
        }
    }
}

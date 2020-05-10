using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleBusiness business;

        public UserRoleController(IUserRoleBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/userrole/addusertorole")]
        public async Task<ActionResult<ResponseResult>> AddUserToRole([FromBody] UserRoleVM model)
        {
            var response = await business.Create(model.UserId, model.RoleId);
            return Ok(response);
        }

        [HttpDelete]
        [ValidateModel]
        [Route("api/userrole/removeuserfromrole")]
        public async Task<ActionResult<ResponseResult>> RemoveUserFromRole([FromBody] UserRoleVM model)
        {
            var response = await business.Delete(model.UserId, model.RoleId);
            return Ok(response);
        }
    }
}
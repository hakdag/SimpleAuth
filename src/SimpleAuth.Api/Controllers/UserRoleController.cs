using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/userrole")]
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
        public async Task<ActionResult<ResponseResult>> Post([FromBody] UserRoleVM model)
        {
            var response = await business.Create(model.UserId, model.RoleId);
            return Ok(response);
        }

        [HttpDelete]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Delete([FromBody] UserRoleVM model)
        {
            var response = await business.Delete(model.UserId, model.RoleId);
            return Ok(response);
        }
    }
}
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
        private readonly IUserRoleService service;

        public UserRoleController(IUserRoleService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] UserRoleVM model)
        {
            var response = await service.Create(model.UserId, model.RoleId);
            return Ok(response);
        }
    }
}
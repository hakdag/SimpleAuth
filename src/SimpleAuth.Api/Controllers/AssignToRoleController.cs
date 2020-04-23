using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/assigntorole")]
    [ApiController]
    public class AssignToRoleController : ControllerBase
    {
        private readonly IAssignToRoleService service;

        public AssignToRoleController(IAssignToRoleService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] AssignToRoleVM model)
        {
            var response = await service.Create(model.UserId, model.RoleId);
            return Ok(response);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.Permission
{
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionBusiness business;

        public RolePermissionController(IRolePermissionBusiness business) => this.business = business;

        [HttpPost]
        [ValidateModel]
        [Route("api/rolepermission/create")]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] RolePermissionVM model)
        {
            var response = await business.Create(model.RoleId, model.PermissionId);
            return Ok(response);
        }

        [HttpDelete]
        [ValidateModel]
        [Route("api/rolepermission/delete")]
        public async Task<ActionResult<ResponseResult>> Delete([FromBody] RolePermissionVM model)
        {
            var response = await business.Delete(model.RoleId, model.PermissionId);
            return Ok(response);
        }
    }
}
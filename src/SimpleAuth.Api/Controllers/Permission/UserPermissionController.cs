using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.Permission
{
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionBusiness business;

        public UserPermissionController(IUserPermissionBusiness business) => this.business = business;

        [HttpPost]
        [ValidateModel]
        [Route("api/userpermission/create")]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] UserPermissionVM model)
        {
            var response = await business.Create(model.UserId, model.PermissionId);
            return Ok(response);
        }

        [HttpDelete]
        [ValidateModel]
        [Route("api/userpermission/delete")]
        public async Task<ActionResult<ResponseResult>> Delete([FromBody] UserPermissionVM model)
        {
            var response = await business.Delete(model.UserId, model.PermissionId);
            return Ok(response);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/changepassword")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IChangePasswordBusiness business;

        public ChangePasswordController(IChangePasswordBusiness business)
        {
            this.business = business;
        }

        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Put([FromBody] ChangePasswordVM model)
        {
            var response = await business.ChangePassword(model.UserName, model.OldPassword, model.Password);
            return Ok(response);
        }
    }
}
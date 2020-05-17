using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models.PasswordReset;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.PasswordReset
{
    [Route("api/passwordreset")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IPasswordResetBusiness business;

        public PasswordResetController(IPasswordResetBusiness business) => this.business = business;

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] PasswordResetVM model)
        {
            var response = await business.Reset(model.UserName, model.ResetKey, model.Password);
            return Ok(response);
        }
    }
}
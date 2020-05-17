using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.PasswordReset
{
    [Route("api/passwordreset/validatekey")]
    [ApiController]
    public class ValidatePasswordResetKeyController : ControllerBase
    {
        private readonly IValidatePasswordResetKeyBusiness business;

        public ValidatePasswordResetKeyController(IValidatePasswordResetKeyBusiness business) => this.business = business;

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] ValidatePasswordResetKeyVM model)
        {
            var response = await business.Validate(model.UserName, model.ResetKey);
            return Ok(response);
        }
    }
}
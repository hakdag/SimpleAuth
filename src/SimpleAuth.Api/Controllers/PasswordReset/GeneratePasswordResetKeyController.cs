using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.PasswordReset
{
    [Route("api/generatepasswordresetkey")]
    [ApiController]
    public class GeneratePasswordResetKeyController : ControllerBase
    {
        private readonly IGeneratePasswordResetKeyBusiness business;

        public GeneratePasswordResetKeyController(IGeneratePasswordResetKeyBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<PasswordResetKeyResponse>> Post([FromBody] GeneratePasswordResetKeyVM model)
        {
            var response = await business.Generate(model.UserName);
            return Ok(response);
        }
    }
}
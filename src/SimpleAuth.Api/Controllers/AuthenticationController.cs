using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness business;

        public AuthenticationController(IAuthenticationBusiness business) => this.business = business;

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticationToken>> Login([FromBody]AuthenticateModel model)
        {
            var token = await business.Authenticate(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Logout([FromBody]LogoutModel model)
        {
            var response = await business.Logout(model.Token);
            if (!response.Success)
            {
                return BadRequest(response.Messages[0]);
            }

            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness service;

        public AuthenticationController(IAuthenticationBusiness service) => this.service = service;

        public async Task<ActionResult<AuthenticationToken>> Post([FromBody]AuthenticateModel model)
        {
            var token = await service.Authenticate(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
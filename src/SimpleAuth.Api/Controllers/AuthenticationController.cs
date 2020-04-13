using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;

namespace SimpleAuth.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService service;

        public AuthenticationController(IAuthenticationService service) => this.service = service;

        public ActionResult<AuthenticationToken> Post([FromBody]AuthenticateModel model)
        {
            var token = service.Authenticate(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
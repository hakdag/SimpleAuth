using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService service;

        public AuthorizationController(IAuthorizationService service) => this.service = service;

        public ActionResult<AuthorizationResult> Post([FromBody]AuthorizationModel model)
        {
            var result = service.ValidateToken(model.Token, model.Secret);
            return Ok(result);
        }
    }
}
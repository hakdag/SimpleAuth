using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationBusiness service;

        public AuthorizationController(IAuthorizationBusiness service) => this.service = service;

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AuthorizationResult>> Post([FromBody]AuthorizationModel model)
        {
            var result = await service.ValidateToken(model.Token);
            return CreatedAtAction(nameof(Post), model, result);
        }
    }
}
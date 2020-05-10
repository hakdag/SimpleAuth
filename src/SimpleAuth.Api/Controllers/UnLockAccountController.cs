using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/unlockaccount")]
    [ApiController]
    public class UnLockAccountController : ControllerBase
    {
        private readonly ILockAccountBusiness business;

        public UnLockAccountController(ILockAccountBusiness business)
        {
            this.business = business;
        }

        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Put([FromBody]LockAccountVM model)
        {
            var response = await business.UnLockAccount(model.UserId);
            return Ok(response);
        }
    }
}
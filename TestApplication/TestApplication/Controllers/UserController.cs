using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthExtensions.Business;
using SimpleAuthExtensions.Service;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISimpleAuthBusiness simpleAuthBusiness;

        public UserController(ISimpleAuthBusiness simpleAuthBusiness)
        {
            this.simpleAuthBusiness = simpleAuthBusiness;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseResult>> LockAccount(TestApplication.Models.UserVM user) => await simpleAuthBusiness.LockAccount(user.UserId);

        [HttpPut]
        public async Task<ActionResult<ResponseResult>> UnLockAccount(TestApplication.Models.UserVM user) => await simpleAuthBusiness.UnLockAccount(user.UserId);
    }
}
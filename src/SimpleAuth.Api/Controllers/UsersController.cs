using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        // TODO: Add pagination
        public async Task<ActionResult<List<UserVM>>> Get()
        {
            var users = await userService.GetAll();
            var userVMs = mapper.Map<List<UserVM>>(users);
            return Ok(userVMs);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] CreateUserVM model)
        {
            var response = await userService.Create(model.UserName, model.Password);
            return Ok(response);
        }
    }
}
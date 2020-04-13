using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Models;
using SimpleAuth.Contracts.Business;
using System.Collections.Generic;

namespace SimpleAuth.Api.Controllers
{
    [Authorize]
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
        public ActionResult<List<UserVM>> Get()
        {
            var users = userService.GetAll();
            var userVMs = mapper.Map<List<UserVM>>(users);
            return Ok(userVMs);
        }
    }
}
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
    [Route("api/users/{id?}")]
    public class UsersController : ControllerBase
    {
        private IUserBusiness business;
        private readonly IMapper mapper;

        public UsersController(IUserBusiness business, IMapper mapper)
        {
            this.business = business;
            this.mapper = mapper;
        }

        // TODO: Add pagination
        public async Task<ActionResult<List<UserVM>>> Get()
        {
            var users = await business.GetAll();
            var userVMs = mapper.Map<List<UserVM>>(users);
            return Ok(userVMs);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] CreateUserVM model)
        {
            var response = await business.Create(model.UserName, model.Password);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var response = await business.Delete(id);
            return Ok(response);
        }
    }
}
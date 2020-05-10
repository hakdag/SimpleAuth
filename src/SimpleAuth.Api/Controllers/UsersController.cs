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
        [HttpGet]
        [Route("api/user/getall")]
        public async Task<ActionResult<List<UserVM>>> GetAll()
        {
            var users = await business.GetAll();
            var userVMs = mapper.Map<List<UserVM>>(users);
            return Ok(userVMs);
        }

        [HttpGet]
        [Route("api/user/get/{id}")]
        public async Task<ActionResult<UserVM>> Get(long id)
        {
            var user = await business.Get(id);
            return Ok(user);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/user/create")]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] CreateUserVM model)
        {
            var response = await business.Create(model.UserName, model.Password);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/user/update")]
        public async Task<ActionResult<ResponseResult>> Update([FromBody] UpdateUserVM model)
        {
            var response = await business.Update(model.UserId, model.NewUserName);
            return Ok(response);
        }

        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(long id)
        {
            var response = await business.Delete(id);
            return Ok(response);
        }
    }
}
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleBusiness business;
        private readonly IMapper mapper;

        public RolesController(IRoleBusiness business, IMapper mapper)
        {
            this.business = business;
            this.mapper = mapper;
        }

        // TODO: Add pagination
        [HttpGet]
        [Route("api/role/getall")]
        public async Task<ActionResult<List<RoleVM>>> GetAll()
        {
            var roles = await business.GetAll();
            var roleVMs = mapper.Map<List<RoleVM>>(roles);
            return Ok(roleVMs);
        }

        [HttpGet]
        [Route("api/role/get/{id}")]
        public async Task<ActionResult<RoleVM>> Get(long id)
        {
            var role = await business.Get(id);
            return Ok(role);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/role/create")]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] CreateRoleVM model)
        {
            var response = await business.Create(model.Name);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/role/update")]
        public async Task<ActionResult<ResponseResult>> Update([FromBody] UpdateRoleVM model)
        {
            var response = await business.Update(model.RoleId, model.NewRoleName);
            return Ok(response);
        }

        [HttpDelete]
        [Route("api/role/delete/{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(long id)
        {
            var response = await business.Delete(id);
            return Ok(response);
        }
    }
}
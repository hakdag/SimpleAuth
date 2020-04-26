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
    [Route("api/role/{id?}")]
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
        public async Task<ActionResult<List<RoleVM>>> Get()
        {
            var roles = await business.GetAll();
            var roleVMs = mapper.Map<List<RoleVM>>(roles);
            return Ok(roleVMs);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] CreateRoleVM model)
        {
            var response = await business.Create(model.Name);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Put([FromBody] UpdateRoleVM model)
        {
            var response = await business.Update(model.RoleId, model.NewRoleName);
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
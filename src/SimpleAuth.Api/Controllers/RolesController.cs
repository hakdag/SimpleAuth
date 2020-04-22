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
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService service;
        private readonly IMapper mapper;

        public RolesController(IRoleService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // TODO: Add pagination
        public async Task<ActionResult<List<RoleVM>>> Get()
        {
            var roles = await service.GetAll();
            var roleVMs = mapper.Map<List<RoleVM>>(roles);
            return Ok(roleVMs);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseResult>> Post([FromBody] CreateRoleVM model)
        {
            var response = await service.Create(model.Name);
            return Ok(response);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Controllers.Permission
{
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionBusiness business;
        private readonly IMapper mapper;

        public PermissionsController(IPermissionBusiness business, IMapper mapper)
        {
            this.business = business;
            this.mapper = mapper;
        }

        // TODO: Add pagination
        [HttpGet]
        [Route("api/permission/getall")]
        public async Task<ActionResult<List<PermissionVM>>> GetAll()
        {
            var permissions = await business.GetAll();
            var permissionVMs = mapper.Map<List<PermissionVM>>(permissions);
            return Ok(permissionVMs);
        }

        [HttpGet]
        [Route("api/permission/get/{id}")]
        public async Task<ActionResult<PermissionVM>> Get(long id)
        {
            var permission = await business.Get(id);
            return Ok(permission);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/permission/create")]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] CreatePermissionVM model)
        {
            var response = await business.Create(model.Name);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/permission/update")]
        public async Task<ActionResult<ResponseResult>> Update([FromBody] UpdatePermissionVM model)
        {
            var response = await business.Update(model.Id, model.NewPermissionName);
            return Ok(response);
        }

        [HttpDelete]
        [Route("api/permission/delete/{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(long id)
        {
            var response = await business.Delete(id);
            return Ok(response);
        }
    }
}
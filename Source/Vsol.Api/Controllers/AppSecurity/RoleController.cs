using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Controllers;

namespace Vsol.Api.AppSecurity
{
    [Route("appSecurity/[controller]")]
	public class RoleController : BaseController
	{
		private readonly IRoleApplicationService _service;
		
		public RoleController(IRoleApplicationService service)
		{
			_service = service;
		}
		
		[HttpGet("{idRole:guid}")]
		public async Task<IActionResult> Get(Guid idRole)
		{
			var item = await _service.GetByIdAsync(idRole);

            if (item == null)
                return NotFound();

            return Ok(item);
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var items = await _service.GetAsync();
			return Ok(items);
		}
		
		[HttpGet("selectlist")]
		public async Task<IActionResult> GetSelectList()
		{
			var items = await _service.GetSelectListAsync();
			return Ok(items);
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Controllers;

namespace Vsol.Api.AppSecurity
{
    [Route("appSecurity/[controller]")]
	public class UserController : BaseController
	{
		private readonly IUserApplicationService _service;
		
		public UserController(IUserApplicationService service)
		{
			_service = service;
		}
		
		[HttpGet("{idUser:guid}")]
		public async Task<IActionResult> Get(Guid idUser)
		{
			var item = await _service.GetByIdAsync(idUser);

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
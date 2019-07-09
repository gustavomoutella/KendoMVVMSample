using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Controllers;

namespace Vsol.Api.AppSecurity
{
    [Route("appSecurity/[controller]")]
	public class AuthorizationController : BaseController
	{
		private readonly IAuthorizationApplicationService _service;
		
		public AuthorizationController(IAuthorizationApplicationService service)
		{
			_service = service;
		}
		
		[HttpGet("{idAuthorization:guid}")]
		public async Task<IActionResult> Get(Guid idAuthorization)
		{
			var item = await _service.GetByIdAsync(idAuthorization);

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
		
        [HttpGet("permissions")]
        [Authorize]
        public async Task<IActionResult> GetPermissions()
        {
            var idUser = base.IdUser.Value;
            var items = await _service.GetPermissionsByUserAsync(idUser);
            return Ok(items);
        }
    }
}
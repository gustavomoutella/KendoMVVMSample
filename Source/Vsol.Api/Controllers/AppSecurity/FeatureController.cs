using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Controllers;

namespace Vsol.Api.AppSecurity
{
    [Route("appSecurity/[controller]")]
	public class FeatureController : BaseController
	{
		private readonly IFeatureApplicationService _service;
		
		public FeatureController(IFeatureApplicationService service)
		{
			_service = service;
		}
		
		[HttpGet("{idFeature:guid}")]
		public async Task<IActionResult> Get(Guid idFeature)
		{
			var item = await _service.GetByIdAsync(idFeature);

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
		
		[HttpGet("recursive")]
		public async Task<IActionResult> GetRecursive()
		{
			var items = await _service.GetRecursiveAsync();
			return Ok(items);
		}

        [HttpGet("parent/{idFeatureParent:guid?}")]
        public async Task<IActionResult> GetByParent(Guid? idFeatureParent)
        {
            var items = await _service.GetByParentAsync(idFeatureParent);
            return Ok(items);
        }
	}
}
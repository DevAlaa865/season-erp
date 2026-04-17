using BranchERP.Application.DTOs;
using BranchERP.Application.DTOs.PermissionDto;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BranchERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] لو حابب تحميه
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var perms = await _permissionService.GetAllAsync();
            return Ok(perms);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var perm = await _permissionService.CreateAsync(model);
            return Ok(perm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var perm = await _permissionService.UpdateAsync(id, model);
            if (perm == null) return NotFound();

            return Ok(perm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _permissionService.DeleteAsync(id);
            if (!ok) return NotFound();

            return Ok();
        }
    }
}

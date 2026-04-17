using BranchERP.Application.DTOs.Auth;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationAdminController : ControllerBase
    {
        private readonly IAuthorizationAdminService _service;

        public AuthorizationAdminController(IAuthorizationAdminService service)
        {
            _service = service;
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions()
            => Ok(await _service.GetAllPermissionsAsync());

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
            => Ok(await _service.GetAllRolesAsync());

        [HttpGet("role-permissions/{roleId}")]
        public async Task<IActionResult> GetRolePermissions(string roleId)
            => Ok(await _service.GetRolePermissionsAsync(roleId));

        [HttpPost("role-permissions")]
        public async Task<IActionResult> UpdateRolePermissions(RolePermissionsUpdateDto dto)
            => Ok(await _service.UpdateRolePermissionsAsync(dto));

        [HttpGet("user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
            => Ok(await _service.GetUserRolesAsync(userId));

        [HttpPost("user-roles")]
        public async Task<IActionResult> UpdateUserRoles(UserRolesUpdateDto dto)
            => Ok(await _service.UpdateUserRolesAsync(dto));

        // ✅ الجديد: إرجاع كل المستخدمين
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
            => Ok(await _service.GetAllUsersAsync());

        [HttpPost("update-user-data")]
        public async Task<IActionResult> UpdateUserData(UpdateUserDataDto dto)
        {
            return Ok(await _service.UpdateUserDataAsync(dto));
        }
    }
}

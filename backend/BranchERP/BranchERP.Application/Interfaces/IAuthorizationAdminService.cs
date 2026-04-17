using BranchERP.Application.DTOs.Auth;

using BranchERP.Application.DTOs.Common;

namespace BranchERP.Application.Interfaces
{
    public interface IAuthorizationAdminService
    {
        Task<ApiResponse<List<PermissionDto>>> GetAllPermissionsAsync();
        Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync();
        Task<ApiResponse<RolePermissionsDto>> GetRolePermissionsAsync(string roleId);
        Task<ApiResponse<bool>> UpdateRolePermissionsAsync(RolePermissionsUpdateDto dto);
        Task<ApiResponse<List<string>>> GetUserRolesAsync(string userId);
        Task<ApiResponse<bool>> UpdateUserRolesAsync(UserRolesUpdateDto dto);

        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();

        // ✔ الجديد
        Task<ApiResponse<bool>> UpdateUserDataAsync(UpdateUserDataDto dto);
    }
}

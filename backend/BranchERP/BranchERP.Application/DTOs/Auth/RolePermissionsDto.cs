namespace BranchERP.Application.DTOs.Auth
{
    public class RolePermissionsDto
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public List<PermissionDto> Permissions { get; set; } = new();
    }
}

namespace BranchERP.Application.DTOs.Auth
{
    public class RolePermissionsUpdateDto
    {
        public string RoleId { get; set; } = string.Empty;

        // List of Permission Ids to assign to this role
        public List<int> PermissionIds { get; set; } = new();
    }
}

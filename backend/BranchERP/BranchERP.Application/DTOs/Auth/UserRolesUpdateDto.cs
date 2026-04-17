namespace BranchERP.Application.DTOs.Auth
{
    public class UserRolesUpdateDto
    {
        public string UserId { get; set; } = string.Empty;

        // أسماء الـ Roles اللي عايز تديها لليوزر
        public List<string> RoleNames { get; set; } = new();
    }
}

using BranchERP.Domain.Entities.Enums;

namespace BranchERP.Application.DTOs.Auth
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? BranchId { get; set; }
        public int? CityId { get; set; }
        public UserType UserType { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsActive { get; set; }
        // اختياري لو عايز تعرضهم في الجدول
        public string? BranchName { get; set; }
        public string? CityName { get; set; }
    }
}

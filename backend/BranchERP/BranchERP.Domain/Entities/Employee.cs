using BranchERP.Domain.Entities;

public class Employee : BaseEntity
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;   // ⭐ تمت إضافته

    public string Gender { get; set; } = "Male";
    public string Position { get; set; } = string.Empty;

    public bool IsSupervisor { get; set; } = false;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
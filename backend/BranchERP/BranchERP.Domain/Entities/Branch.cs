using BranchERP.Domain.Entities;
using BranchERP.Domain.Enums;

public class Branch : BaseEntity
{
    public string BranchName { get; set; } = string.Empty;

    public int BranchNumber { get; set; }
    public BranchType BranchType { get; set; }

    // City
    public int CityId { get; set; }
    public City City { get; set; }

    // ActivityType
    public int ActivityTypeId { get; set; }
    public ActivityType ActivityType { get; set; }

    // Supervisor (اختياري)
    public int? SupervisorId { get; set; }
    public Employee? Supervisor { get; set; }   // ✔ مهم جدًا

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

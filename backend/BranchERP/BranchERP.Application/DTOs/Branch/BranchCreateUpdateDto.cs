using BranchERP.Domain.Enums;

public class BranchCreateUpdateDto
{
    public string BranchName { get; set; } = string.Empty;
    public int BranchNumber { get; set; }
    public BranchType BranchType { get; set; }

    public int CityId { get; set; }
    public int ActivityTypeId { get; set; }

    public int? SupervisorId { get; set; }

    public bool IsActive { get; set; } = true;

    // ⭐ الجديد
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}
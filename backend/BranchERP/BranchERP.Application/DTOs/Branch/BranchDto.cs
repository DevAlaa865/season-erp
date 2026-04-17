using BranchERP.Domain.Enums;

public class BranchDto
{
    public int Id { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public int BranchNumber { get; set; }
    public BranchType BranchType { get; set; }

    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;

    public int ActivityTypeId { get; set; }
    public string ActivityTypeName { get; set; } = string.Empty;

    public int? SupervisorId { get; set; }
    public string? SupervisorName { get; set; }

    public bool IsActive { get; set; }

    // ⭐ الجديد
    public DateTime CreatedAt { get; set; }

}

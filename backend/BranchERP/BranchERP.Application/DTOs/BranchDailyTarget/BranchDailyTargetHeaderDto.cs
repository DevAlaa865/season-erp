namespace BranchERP.Application.DTOs.BranchDailyTarget;
public class BranchDailyTargetHeaderDto
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public string? BranchName { get; set; }
    public DateTime TargetDate { get; set; }

    public decimal? TotalBranchTarget { get; set; }
    public decimal? TotalAchieved { get; set; }
    public decimal? AchievementPercentage { get; set; }
    public decimal? BranchCommission { get; set; }

    public IReadOnlyList<BranchDailyTargetDetailDto> Details { get; set; }
        = new List<BranchDailyTargetDetailDto>();
}

using BranchERP.Domain.Entities;

public class BranchDailyTargetHeader : BaseEntity
{
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public DateTime TargetDate { get; set; }

    public decimal? TotalBranchTarget { get; set; } = 0;
    public decimal? TotalAchieved { get; set; } = 0;
    public decimal? AchievementPercentage { get; set; } = 0;
    public decimal? BranchCommission { get; set; } = 0;

    public ICollection<BranchDailyTargetDetail> Details { get; set; } = new List<BranchDailyTargetDetail>();
}

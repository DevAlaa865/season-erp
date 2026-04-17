
namespace BranchERP.Application.DTOs.BranchDailyTarget; 
public class BranchDailyTargetHeaderCreateUpdateDto
{
    public int BranchId { get; set; }
    public DateTime TargetDate { get; set; }

    public decimal? TotalBranchTarget { get; set; }
    public decimal? TotalAchieved { get; set; }

    public List<BranchDailyTargetDetailCreateUpdateDto> Details { get; set; }
        = new();
}

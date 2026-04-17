
namespace BranchERP.Application.DTOs.BranchDailyTarget;

public class BranchDailyTargetDetailDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    public int Shift { get; set; }

    public decimal? EmployeeTarget { get; set; }
    public decimal? EmployeeAchieved { get; set; }
    public decimal? EmployeeAchievementPercentage { get; set; }
    public decimal? EmployeeCommission { get; set; }
}

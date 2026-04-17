
namespace BranchERP.Application.DTOs.BranchDailyTarget;
public class BranchDailyTargetDetailCreateUpdateDto
{
    public int EmployeeId { get; set; }
    public int Shift { get; set; }

    public decimal? EmployeeTarget { get; set; }
    public decimal? EmployeeAchieved { get; set; }
}

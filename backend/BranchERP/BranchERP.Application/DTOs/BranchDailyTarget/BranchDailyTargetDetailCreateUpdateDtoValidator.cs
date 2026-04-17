using FluentValidation;

namespace BranchERP.Application.DTOs.BranchDailyTarget
{
    public class BranchDailyTargetDetailCreateUpdateDtoValidator : AbstractValidator<BranchDailyTargetDetailCreateUpdateDto>
    {
        public BranchDailyTargetDetailCreateUpdateDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId is required");

            RuleFor(x => x.Shift)
                .GreaterThan(0).WithMessage("Shift must be greater than 0");

            RuleFor(x => x.EmployeeTarget)
                .GreaterThanOrEqualTo(0).When(x => x.EmployeeTarget.HasValue);

            RuleFor(x => x.EmployeeAchieved)
                .GreaterThanOrEqualTo(0).When(x => x.EmployeeAchieved.HasValue);
        }
    }
}

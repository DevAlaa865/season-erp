using FluentValidation;

namespace BranchERP.Application.DTOs.BranchDailyTarget
{
    public class BranchDailyTargetHeaderCreateUpdateDtoValidator : AbstractValidator<BranchDailyTargetHeaderCreateUpdateDto>
    {
        public BranchDailyTargetHeaderCreateUpdateDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("BranchId is required");

            RuleFor(x => x.TargetDate)
                .NotEmpty().WithMessage("TargetDate is required");

            RuleForEach(x => x.Details)
                .SetValidator(new BranchDailyTargetDetailCreateUpdateDtoValidator());
        }
    }
}

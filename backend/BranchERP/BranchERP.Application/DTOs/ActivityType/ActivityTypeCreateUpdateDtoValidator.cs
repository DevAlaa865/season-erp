using FluentValidation;

namespace BranchERP.Application.DTOs.ActivityType
{
    public class ActivityTypeCreateUpdateDtoValidator : AbstractValidator<ActivityTypeCreateUpdateDto>
    {
        public ActivityTypeCreateUpdateDtoValidator()
        {
            RuleFor(x => x.ActivityName)
                .NotEmpty().WithMessage("ActivityName is required")
                .MinimumLength(3).WithMessage("ActivityName must be at least 3 characters")
                .MaximumLength(100).WithMessage("ActivityName must not exceed 100 characters");
        }
    }
}

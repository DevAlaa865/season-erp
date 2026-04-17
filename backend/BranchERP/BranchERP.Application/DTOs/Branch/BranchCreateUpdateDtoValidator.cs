using FluentValidation;

namespace BranchERP.Application.DTOs.Branch
{
    public class BranchCreateUpdateDtoValidator : AbstractValidator<BranchCreateUpdateDto>
    {
        public BranchCreateUpdateDtoValidator()
        {
            RuleFor(x => x.BranchName)
                .NotEmpty().MinimumLength(3);

            RuleFor(x => x.BranchNumber)
                .GreaterThan(0);

            RuleFor(x => x.BranchType)
                .IsInEnum();

            RuleFor(x => x.CityId)
                .GreaterThan(0);

            RuleFor(x => x.ActivityTypeId)
                .GreaterThan(0);

            RuleFor(x => x.SupervisorId)
                .GreaterThan(0)
                .When(x => x.SupervisorId.HasValue);

            // ⭐ الجديد
            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}

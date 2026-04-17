using FluentValidation;

namespace BranchERP.Application.DTOs.ShortageType
{
    public class ShortageTypeCreateUpdateDtoValidator : AbstractValidator<ShortageTypeCreateUpdateDto>
    {
        public ShortageTypeCreateUpdateDtoValidator()
        {
            RuleFor(x => x.ShortageName)
                .NotEmpty().WithMessage("ShortageName is required")
                .MinimumLength(3).WithMessage("ShortageName must be at least 3 characters")
                .MaximumLength(100).WithMessage("ShortageName must not exceed 100 characters");
        }
    }
}

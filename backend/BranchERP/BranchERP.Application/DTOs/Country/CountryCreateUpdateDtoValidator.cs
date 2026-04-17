using FluentValidation;

namespace BranchERP.Application.DTOs.Country
{
    public class CountryCreateUpdateDtoValidator : AbstractValidator<CountryCreateUpdateDto>
    {
        public CountryCreateUpdateDtoValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty().WithMessage("CountryName is required")
                .MinimumLength(3).WithMessage("CountryName must be at least 3 characters")
                .MaximumLength(100).WithMessage("CountryName must not exceed 100 characters");
        }
    }
}

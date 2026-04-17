using FluentValidation;

namespace BranchERP.Application.DTOs.Employee
{
    public class EmployeeCreateUpdateDtoValidator : AbstractValidator<EmployeeCreateUpdateDto>
    {
        public EmployeeCreateUpdateDtoValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("EmployeeCode is required");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required")
                .MinimumLength(3).WithMessage("FullName must be at least 3 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^[0-9]+$").WithMessage("Phone must contain only digits")
                .MinimumLength(8).WithMessage("Phone must be at least 8 digits");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be Male or Female");
        }
    }
}

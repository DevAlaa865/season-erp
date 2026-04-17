using FluentValidation;
using BranchERP.Domain.Entities.Enums;

namespace BranchERP.Application.DTOs.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("DisplayName is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            // 🔥 أهم إضافة — حل مشكلة الـ Enum 100%
            RuleFor(x => x.UserType)
                .IsInEnum()
                .WithMessage("Invalid user type value");
        }
    }
}

using FluentValidation;

namespace BranchERP.Application.DTOs.BranchSalesDaily
{
    public class BranchSalesDailyCreateUpdateDtoValidator : AbstractValidator<BranchSalesDailyCreateUpdateDto>
    {
        public BranchSalesDailyCreateUpdateDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("BranchId is required");

            RuleFor(x => x.SalesDate)
                .NotEmpty().WithMessage("SalesDate is required");

            RuleForEach(x => x.ShortageDetails).ChildRules(detail =>
            {
                detail.RuleFor(d => d.ShortageTypeId)
                    .GreaterThan(0).WithMessage("ShortageTypeId is required");

                detail.RuleFor(d => d.Amount)
                    .GreaterThanOrEqualTo(0).When(d => d.Amount.HasValue);
            });
        }
    }
}
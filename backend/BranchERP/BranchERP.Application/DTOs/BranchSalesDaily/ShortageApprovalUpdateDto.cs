namespace BranchERP.Application.DTOs.BranchSalesDaily
{
    public class ShortageApprovalUpdateDto
    {
        public int Id { get; set; }              // Id بتاع BranchSalesShortageDetail
        public bool? IsReturnApproved { get; set; }
        public bool? IsDiscountApproved { get; set; }

        public string? ReturnNotes { get; set; }
        public string? DiscountNotes { get; set; }
    }
}

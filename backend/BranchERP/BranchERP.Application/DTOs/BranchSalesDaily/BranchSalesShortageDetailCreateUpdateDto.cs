// ============================
// BranchSalesShortageDetailCreateUpdateDto
// ============================
namespace BranchERP.Application.DTOs.BranchSalesDaily
{
    public class BranchSalesShortageDetailCreateUpdateDto
    {
        public int ShortageTypeId { get; set; }
        public decimal? Amount { get; set; }

        // 🔥 مسار صورة إثبات العجز (اختياري)
        public string? AttachmentPath { get; set; }

        // 🔥 الموظف المرتبط بالعجز (اختياري)
        public int? EmployeeId { get; set; }

        public bool? IsReturnApproved { get; set; }
        public bool? IsDiscountApproved { get; set; }

        public string? ReturnNotes { get; set; }
        public string? DiscountNotes { get; set; }
    }
}
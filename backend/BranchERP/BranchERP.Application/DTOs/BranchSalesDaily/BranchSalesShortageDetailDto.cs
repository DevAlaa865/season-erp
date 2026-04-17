// ============================
// BranchSalesShortageDetailDto
// ============================
namespace BranchERP.Application.DTOs.BranchSalesDaily
{
    public class BranchSalesShortageDetailDto
    {
        public int Id { get; set; }

        public int ShortageTypeId { get; set; }
        public string ShortageTypeName { get; set; } = string.Empty;

        public decimal? Amount { get; set; }

        // 🔥 مسار صورة إثبات العجز (اختياري)
        public string? AttachmentPath { get; set; }

        // 🔥 بيانات الموظف
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }

        public bool? IsReturnApproved { get; set; }
        public bool? IsDiscountApproved { get; set; }

        public string? ReturnNotes { get; set; }
        public string? DiscountNotes { get; set; }
    }
}
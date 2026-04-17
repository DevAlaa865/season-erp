namespace BranchERP.Domain.Entities
{
    public class BranchSalesShortageDetail : BaseEntity
    {
        // الربط مع اليومية
        public int BranchSalesDailyId { get; set; }
        public BranchSalesDaily BranchSalesDaily { get; set; }

        // نوع العجز
        public int ShortageTypeId { get; set; }
        public ShortageType ShortageType { get; set; }

        // 🔥 الموظف (اختياري)
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // مبلغ العجز (اختياري)
        public decimal? Amount { get; set; } = 0;

        // مسار الصورة المرفقة
        public string? AttachmentPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // اعتماد إدارة المرتجعات
        public bool? IsReturnApproved { get; set; }

        // اعتماد إدارة الخصومات
        public bool? IsDiscountApproved { get; set; }

        // ملاحظات إدارة المرتجعات
        public string? ReturnNotes { get; set; }

        // ملاحظات إدارة الخصومات
        public string? DiscountNotes { get; set; }
    }
}

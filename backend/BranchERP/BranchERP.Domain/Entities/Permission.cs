namespace BranchERP.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = string.Empty;   // اسم الصلاحية
        public string Code { get; set; } = string.Empty;   // كود ثابت مثل: Branches.View
    }
}

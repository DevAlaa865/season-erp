namespace BranchERP.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // ممكن بعدين تزود:
        // public string? Description { get; set; }
    }
}

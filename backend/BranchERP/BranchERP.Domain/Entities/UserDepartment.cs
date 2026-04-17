namespace BranchERP.Domain.Entities
{
    public class UserDepartment
    {
        public string UserId { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
    }
}

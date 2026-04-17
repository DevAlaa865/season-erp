namespace BranchERP.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Gender { get; set; } = "Male";
        public string Position { get; set; } = string.Empty;

        public bool IsSupervisor { get; set; }
        public bool IsActive { get; set; }
    }
}

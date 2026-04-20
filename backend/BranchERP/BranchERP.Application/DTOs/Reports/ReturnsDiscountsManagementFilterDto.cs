namespace BranchERP.Application.DTOs.Reports
{
    public class ReturnsDiscountsManagementFilterDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int? CityId { get; set; }   // أو CityId لو ده اللي عندك
        public int? BranchId { get; set; }
    }
}

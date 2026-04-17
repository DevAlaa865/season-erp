namespace BranchERP.Application.DTOs.Region
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string RegionName { get; set; } = string.Empty;

        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
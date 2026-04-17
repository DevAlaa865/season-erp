namespace BranchERP.Application.DTOs.City
{
    public class CityDto
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;

        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
}

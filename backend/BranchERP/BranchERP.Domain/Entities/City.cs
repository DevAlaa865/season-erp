namespace BranchERP.Domain.Entities
{
    public class City : BaseEntity
    {
        public string CityName { get; set; } = string.Empty;

        // الربط مع Country
        public int CountryId { get; set; }
        public Country Country { get; set; }

        // المناطق التابعة للمدينة
        public ICollection<Region> Regions { get; set; } = new List<Region>();
    }
}

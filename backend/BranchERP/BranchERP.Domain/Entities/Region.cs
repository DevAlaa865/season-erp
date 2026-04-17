namespace BranchERP.Domain.Entities
{
    public class Region : BaseEntity
    {
        public string RegionName { get; set; } = string.Empty;

        // الربط مع City
        public int CityId { get; set; }
        public City City { get; set; }


    }
}

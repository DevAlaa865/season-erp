namespace BranchERP.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; } = string.Empty;

        // المدن التابعة للبلد
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}

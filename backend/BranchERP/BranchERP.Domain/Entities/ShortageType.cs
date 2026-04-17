namespace BranchERP.Domain.Entities
{
    public class ShortageType : BaseEntity
    {

        public string ShortageName { get; set; } = string.Empty; // مثال: خصم فواتير، مكافأة، مصروف فرع

        public bool IsActive { get; set; } = true;
    }
}

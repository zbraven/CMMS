using CMMS.Domain.Enums;


namespace CMMS.Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public MaintenanceFrequency MaintenanceFrequency { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}

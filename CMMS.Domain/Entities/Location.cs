

namespace CMMS.Domain.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationCity { get; set; }
        public string LocationDistrict { get; set; }
        public string LocationZipCode { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}

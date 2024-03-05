

namespace CMMS.Domain.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}

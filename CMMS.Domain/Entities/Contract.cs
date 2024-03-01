namespace CMMS.Domain.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string Name { get; set; }
        public virtual Asset Asset { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Terms { get; set; }
        public int EmployeeId { get; set; } 
        public virtual Employee ResponsibleEmployee { get; set; } 
    }
}

namespace CMMS.Domain.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int MaintenanceTaskId { get; set; }
        public virtual MaintenanceTask MaintenanceTask { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}

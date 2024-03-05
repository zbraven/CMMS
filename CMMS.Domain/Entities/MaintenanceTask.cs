using CMMS.Domain.Enums;

namespace CMMS.Domain.Entities
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public MaintenanceTaskStatus Status { get; set; }
        public MaintenancePriority MaintenancePrioritys { get; set; }   
        public virtual Asset Asset { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TaskMaterial> TaskMaterials { get; set; }
        public virtual ICollection<Employee> AssignedEmployees { get; set; }
        public bool IsPlanned { get; set; }
    }
}


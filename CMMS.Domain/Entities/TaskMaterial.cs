using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Entities
{
    public class TaskMaterial
    {
        public int Id { get; set; }
        public int MaintenanceTaskId { get; set; }
        public virtual MaintenanceTask MaintenanceTasks { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int Quantity { get; set; }
    }
}

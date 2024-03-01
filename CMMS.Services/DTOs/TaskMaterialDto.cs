using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class TaskMaterialDto
    {
        public int Id { get; set; }
        public int MaintenanceTaskId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
    }
}

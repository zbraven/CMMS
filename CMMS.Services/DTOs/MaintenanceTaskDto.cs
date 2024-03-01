using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class MaintenanceTaskDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public MaintenanceTaskStatus Status { get; set; }
        public MaintenancePriority MaintenancePriority { get; set; }
        public string Description { get; set; }
    }
}

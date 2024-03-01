using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class PlanDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public MaintenanceFrequency MaintenanceFrequency { get; set; }
    }
}

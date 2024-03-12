using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime QualityDate { get; set; }
        public AssetStatus Status { get; set; }
        public AssetType Type { get; set; }
        public int? LocationId { get; set; }
        //public virtual Location Location { get; set; }
        public int? PlanId { get; set; }
        //public virtual Plan Plan { get; set; }
        //public virtual ICollection<MaintenanceTask> MaintenanceTasks { get; set; }
        //public virtual ICollection<Contract> Contracts { get; set; }





    }
}

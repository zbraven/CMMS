using CMMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace CMMS.Services.DTOs
{
    public class ActivityLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Notes { get; set; }
        public int EmployeeId { get; set; }

        [ValidateNever]
        public int MaintenanceTaskId { get; set; }
        [ValidateNever]
        public virtual MaintenanceTask MaintenanceTask { get; set; }

        [ValidateNever]
        public virtual ICollection<Report> Reports { get; set; }
    }

}
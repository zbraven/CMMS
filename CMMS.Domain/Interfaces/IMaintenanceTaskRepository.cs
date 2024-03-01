using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IMaintenanceTaskRepository : IRepository<MaintenanceTask>
    {
        Task<IEnumerable<MaintenanceTask>> GetTasksByStatusAsync(MaintenanceTaskStatus status);
        Task<IEnumerable<MaintenanceTask>> GetTasksByAssetIdAsync(int assetId);
        Task<IEnumerable<MaintenanceTask>> GetTasksByPriorityAsync(MaintenancePriority priority);
    }
}

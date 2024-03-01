using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IMaintenanceTaskService
    {
        Task<IEnumerable<MaintenanceTaskDto>> GetAllMaintenanceTasksAsync();
        Task<MaintenanceTaskDto> GetMaintenanceTaskByIdAsync(int taskId);
        Task<MaintenanceTaskDto> CreateMaintenanceTaskAsync(MaintenanceTaskDto taskDto);
        Task UpdateMaintenanceTaskAsync(MaintenanceTaskDto taskDto);
        Task DeleteMaintenanceTaskAsync(int taskId);
    }
}

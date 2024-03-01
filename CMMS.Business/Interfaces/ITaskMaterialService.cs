using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface ITaskMaterialService
    {
        Task<IEnumerable<TaskMaterialDto>> GetAllTaskMaterialsAsync();
        Task<TaskMaterialDto> GetTaskMaterialByIdAsync(int taskMaterialId);
        Task<TaskMaterialDto> CreateTaskMaterialAsync(TaskMaterialDto taskMaterialDto);
        Task UpdateTaskMaterialAsync(TaskMaterialDto taskMaterialDto);
        Task DeleteTaskMaterialAsync(int taskMaterialId);
    }
}

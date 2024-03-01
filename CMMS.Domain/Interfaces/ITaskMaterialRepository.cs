using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface ITaskMaterialRepository : IRepository<TaskMaterial>
    {
        Task<IEnumerable<TaskMaterial>> GetMaterialsByTaskIdAsync(int taskId);
        Task<IEnumerable<TaskMaterial>> GetTasksByMaterialIdAsync(int materialId);

    }
}

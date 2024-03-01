using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class TaskMaterialRepository : Repository<TaskMaterial>, ITaskMaterialRepository
    {
        private readonly CMMSDbContext _context;
        public TaskMaterialRepository(CMMSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskMaterial>> GetMaterialsByTaskIdAsync(int taskId)
        {
            return await _context.Set<TaskMaterial>().Where(x => x.MaintenanceTaskId == taskId).Include(y => y.Material).ToListAsync();
        }

        public async Task<IEnumerable<TaskMaterial>> GetTasksByMaterialIdAsync(int materialId)
        {
            return await _context.Set<TaskMaterial>().Where(x => x.MaterialId == materialId).Include(y => y.MaintenanceTasks).ToListAsync();
        }
    }

}

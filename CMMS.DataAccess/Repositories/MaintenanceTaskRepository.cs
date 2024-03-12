using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class MaintenanceTaskRepository : Repository<MaintenanceTask>, IMaintenanceTaskRepository
    {
        private readonly CMMSDbContext _context;


        public MaintenanceTaskRepository(CMMSDbContext context):base(context)
        {
            _context=context;
        }

    

        public async Task<IEnumerable<MaintenanceTask>> GetTasksByAssetIdAsync(int assetId)
        {
            return await _context.Set<MaintenanceTask>().Where(x=>x.AssetId == assetId).ToListAsync();   
        }

        public async Task<IEnumerable<MaintenanceTask>> GetTasksByPriorityAsync(MaintenancePriority priority)
        {
            return await _context.Set<MaintenanceTask>().Where(x => x.MaintenancePrioritys == priority).ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceTask>> GetTasksByStatusAsync(MaintenanceTaskStatus status)
        {
            return await _context.Set<MaintenanceTask>().Where(x => x.Status == status).ToListAsync();
        }


        public async Task<int> CountTasksByMonthAsync(int month, int year)
        {
            return await _context.MaintenanceTasks
                                 .CountAsync(t => t.Date.Month == month && t.Date.Year == year);
        }
    }
}

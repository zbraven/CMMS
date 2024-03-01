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
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly CMMSDbContext _context;
        public ReportRepository(CMMSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Report>> GetReportsByActivityLogIdAsync(int activityLogId)
        {
            return await _context.Set<Report>().Where(x => x.ActivityLogId == activityLogId).ToListAsync();
        }

        // MaintenanceTask ile ilişkili ActivityLog'lar üzerinden filtreleme yaparak raporları getirir
        public async Task<IEnumerable<Report>> GetPhotosByTaskIdAsync(int taskId)
        {
           
            return await _context.Set<Report>()
                .Include(x => x.ActivityLog)
                .Where(x => x.ActivityLog.MaintenanceTaskId == taskId)
                .ToListAsync();
        }
    }

}

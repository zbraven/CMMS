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
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository
    {
        private readonly CMMSDbContext _context;

        public ActivityLogRepository(CMMSDbContext context):base(context)
        {
            _context = context;
        }



        public async Task<IEnumerable<ActivityLog>> GetEmployeeWorks(int employeeId)
        {
            return await _context.Set<ActivityLog>().Where(x => x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<ActivityLog>> GetRecentLogsAsync(int count)
        {
            // Burada en son eklenen logları belirli bir sayıda getiren bir kod yazılmalıdır.
            // Örnek olarak, son `count` kadar logu tarih sırasına göre alabiliriz.

            // Örnek kod:
            var recentLogs = await _context.ActivityLogs
                .OrderByDescending(log => log.Date) // Tarihe göre azalan şekilde sırala
                .Take(count) // Belirli sayıda log al
                .ToListAsync(); // Sonuçları liste olarak dön

            return recentLogs;
        }

        public async Task<IEnumerable<ActivityLog>> GetStartAndFinishDateLogsAsync(DateTime startDate, DateTime finishDate)
        {
            return await _context.Set<ActivityLog>().Where(x => x.Date>= startDate && x.Date<= finishDate).ToListAsync();   
        }

        

        public async Task<IEnumerable<ActivityLog>> GetRecentActivityLogsAsync(int count)
        {
            return await _context.ActivityLogs
                                 .OrderByDescending(a => a.Date) // Tarihe göre en son eklenenlerden başla
                                 .Take(count) // Sadece istenilen sayıda log al
                                 .ToListAsync();
        }

    }
}



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

        public async Task<IEnumerable<ActivityLog>> GetStartAndFinishDateLogsAsync(DateTime startDate, DateTime finishDate)
        {
            return await _context.Set<ActivityLog>().Where(x => x.Date>= startDate && x.Date<= finishDate).ToListAsync();   
        }
    }
}



using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IActivityLogRepository: IRepository<ActivityLog>
    {
        Task<IEnumerable<ActivityLog>> GetEmployeeWorks(int employeeId); 
        Task<IEnumerable<ActivityLog>> GetStartAndFinishDateLogsAsync(DateTime startDate, DateTime finishDate);
    }
}

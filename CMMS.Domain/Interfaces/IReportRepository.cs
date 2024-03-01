using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{

    public interface IReportRepository : IRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportsByActivityLogIdAsync(int activityLogId);
        Task<IEnumerable<Report>> GetPhotosByTaskIdAsync(int taskId);

    }
}

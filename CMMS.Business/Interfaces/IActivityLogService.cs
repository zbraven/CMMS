using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IActivityLogService
    {
        Task<IEnumerable<ActivityLogDto>> GetAllActivityLogsAsync();
        Task<ActivityLogDto> GetActivityLogByIdAsync(int logId);
        Task<ActivityLogDto> CreateActivityLogAsync(ActivityLogDto logDto);
        Task UpdateActivityLogAsync(ActivityLogDto logDto);
        Task DeleteActivityLogAsync(int logId);
    }
}

using CMMS.Domain.Enums;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IDashboardService
    {
        Task<int> GetTotalAssetsCountAsync();
        Task<decimal> GetAssetCountIncreasePercentageAsync();
        Task<int> GetActiveUsersCountAsync();
        Task<int> GetPlannedTasksCountAsync();
        Task<decimal> GetTaskCountIncreasePercentageAsync();
        Task<decimal> GetTotalCostForLastYearAsync();
        Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync();
        Task<Dictionary<string, int>> GetAssetTypeDistributionAsync();
        Task<decimal> GetCostIncreaseFromPreviousMonthAsync();
        Task<decimal> GetCostIncreaseFromPreviousYearAsync();
        Task<decimal> GetTotalCostAllTimeAsync();
    }
}
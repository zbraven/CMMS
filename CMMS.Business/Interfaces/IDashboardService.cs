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
        Task<int> GetTotalMaintenanceTaskCountAsync();
        Task<int> GetTotalUserCountAsync();
        Task<int> GetActiveUsersCountAsync();
        Task<IEnumerable<AssetTypeDistributionDto>> GetAssetTypeDistributionAsync();
        Task<decimal> GetCostIncreaseFromPreviousMonthAsync();
        Task<decimal> GetTotalCostAllTimeAsync();
        Task<decimal> GetUserCountIncreasePercentageAsync();
        Task<decimal> GetCostIncreaseFromPreviousYearAsync();
        Task<decimal> GetAssetCountIncreasePercentageAsync();
        Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync();
        Task<decimal> GetTaskCountIncreasePercentageAsync();
    }
}
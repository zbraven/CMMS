using CMMS.Services.DTOs;

public class DashboardViewModel
{
    public int TotalAssetsCount { get; set; }
    public int TotalMaintenanceTaskCount { get; set; }
    public int TotalUserCount { get; set; }
    public int ActiveUsersCount { get; set; }
    public IEnumerable<AssetTypeDistributionDto> AssetTypeDistribution { get; set; }
    public decimal CostIncreaseFromPreviousMonth { get; set; }
    public decimal TotalCostAllTime { get; set; }
    public decimal UserCountIncreasePercentage { get; set; }
    public decimal CostIncreaseFromPreviousYear { get; set; }
    public decimal AssetCountIncreasePercentage { get; set; }
    public IEnumerable<ActivityLogDto> RecentActivityLogs { get; set; }
    public decimal TaskCountIncreasePercentage { get; set; }
}
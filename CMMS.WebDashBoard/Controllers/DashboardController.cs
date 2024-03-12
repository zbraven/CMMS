using CMMS.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalAssetsCount = await _dashboardService.GetTotalAssetsCountAsync(),
                TotalMaintenanceTaskCount = await _dashboardService.GetTotalMaintenanceTaskCountAsync(),
                TotalUserCount = await _dashboardService.GetTotalUserCountAsync(),
                ActiveUsersCount = await _dashboardService.GetActiveUsersCountAsync(),
                AssetTypeDistribution = await _dashboardService.GetAssetTypeDistributionAsync(),
                CostIncreaseFromPreviousMonth = await _dashboardService.GetCostIncreaseFromPreviousMonthAsync(),
                TotalCostAllTime = await _dashboardService.GetTotalCostAllTimeAsync(),
                UserCountIncreasePercentage = await _dashboardService.GetUserCountIncreasePercentageAsync(),
                CostIncreaseFromPreviousYear = await _dashboardService.GetCostIncreaseFromPreviousYearAsync(),
                AssetCountIncreasePercentage = await _dashboardService.GetAssetCountIncreasePercentageAsync(),
                RecentActivityLogs = await _dashboardService.GetRecentActivityLogsAsync(),
                TaskCountIncreasePercentage = await _dashboardService.GetTaskCountIncreasePercentageAsync(),
            };

            return View(viewModel);
        }
    }



}

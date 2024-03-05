using CMMS.Business.Interfaces;
using CMMS.DataAccess.Repositories;
using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using CMMS.Domain.Interfaces;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaintenanceTaskRepository _taskRepository;
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IPlanRepository _planRepository;
        public DashboardService(IEmployeeRepository employeeRepository, IAssetRepository assetRepository, MaterialRepository materialRepository, IMaintenanceTaskRepository taskRepository, IActivityLogRepository activityLogRepository, IPlanRepository planRepository)
        {
            _employeeRepository = employeeRepository;
            _assetRepository = assetRepository;
            _materialRepository = materialRepository;
            _taskRepository = taskRepository;   
            _activityLogRepository = activityLogRepository;
            _planRepository = planRepository;
        }

        //Aktif Kullanıcıların Sayısı
        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _employeeRepository.CountAsync(u => u.IsActive);
        }

        public async Task<decimal> GetAssetCountIncreasePercentageAsync()
        {
            // Geçen ayki ve bu ayki varlık sayılarına göre yüzde artışı hesaplar
            var lastMonthAssetCount = await _assetRepository.CountAsync(a => a.AcceptedDate.Month == DateTime.Now.AddMonths(-1).Month && a.AcceptedDate.Year == DateTime.Now.AddMonths(-1).Year);
            var thisMonthAssetCount = await _assetRepository.CountAsync(a => a.AcceptedDate.Month == DateTime.Now.Month && a.AcceptedDate.Year == DateTime.Now.Year);

            if (lastMonthAssetCount == 0) return 100; // Eğer geçen ay varlık eklenmediyse, %100 artış olduğunu varsayabiliriz.

            return ((thisMonthAssetCount - lastMonthAssetCount) / (decimal)lastMonthAssetCount) * 100;
        }

        public async Task<Dictionary<string, int>> GetAssetTypeDistributionAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var distribution = new Dictionary<string, int>();

            foreach (var asset in assets)
            {
                var assetType = asset.Type.ToString();
                if (distribution.ContainsKey(assetType))
                {
                    distribution[assetType]++;
                }
                else
                {
                    distribution[assetType] = 1;
                }
            }

            return distribution;
        }

        public async Task<decimal> GetCostIncreaseFromPreviousMonthAsync()
        {
            // Önceki aya göre maliyet artışını hesaplar
            var lastMonthStartDate = DateTime.Now.AddMonths(-2).Date; // Geçen ayın başlangıç tarihi
            var lastMonthEndDate = DateTime.Now.AddMonths(-1).Date.AddDays(-1); // Geçen ayın son gününün son saati
            var thisMonthStartDate = DateTime.Now.AddMonths(-1).Date; // Bu ayın başlangıç tarihi
            var thisMonthEndDate = DateTime.Now.Date; // Bu ayın son gününün son saati

            var lastMonthTotalCost = await _materialRepository.GetTotalCostAsync(lastMonthStartDate, lastMonthEndDate);
            var thisMonthTotalCost = await _materialRepository.GetTotalCostAsync(thisMonthStartDate, thisMonthEndDate);

            if (lastMonthTotalCost == 0) return 100; // Eğer geçen ay maliyet yoksa, %100 artış olduğunu varsayabiliriz.

            return ((thisMonthTotalCost - lastMonthTotalCost) / lastMonthTotalCost) * 100;
        }

        public async Task<decimal> GetCostIncreaseFromPreviousYearAsync()
        {
            // Geçen yıla göre maliyet artışını hesaplar
            var lastYearTotalCost = await _materialRepository.GetTotalCostAsync(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 12, 31));
            var thisYearTotalCost = await _materialRepository.GetTotalCostAsync(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);

            if (lastYearTotalCost == 0) return 100; // Eğer geçen yıl maliyet yoksa, %100 artış olduğunu varsayabiliriz.

            return ((thisYearTotalCost - lastYearTotalCost) / lastYearTotalCost) * 100;
        }

        public async Task<int> GetPlannedTasksCountAsync()
        {
            // Planlanmış görev sayısını döndürür
            return await _taskRepository.CountAsync(t => t.IsPlanned);
        }

        public async Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync()
        {
            // Son 10 aktivite logunu getirir
            var recentLogs = await _activityLogRepository.GetRecentLogsAsync(10);
            return recentLogs.Select(log => new ActivityLogDto
            {
                // Mapping işlemleri
            }).ToList();
        }

        public async Task<decimal> GetTaskCountIncreasePercentageAsync()
        {
            // Geçen ayki ve bu ayki görev sayılarına göre yüzde artışı hesaplar
            var lastMonthTaskCount = await _taskRepository.CountAsync(t => t.Date.Month == DateTime.Now.AddMonths(-1).Month && t.Date.Year == DateTime.Now.AddMonths(-1).Year);
            var thisMonthTaskCount = await _taskRepository.CountAsync(t => t.Date.Month == DateTime.Now.Month && t.Date.Year == DateTime.Now.Year);

            if (lastMonthTaskCount == 0) return 100; // Eğer geçen ay görev eklenmediyse, %100 artış olduğunu varsayabiliriz.

            return ((thisMonthTaskCount - lastMonthTaskCount) / (decimal)lastMonthTaskCount) * 100;
        }

        public async Task<int> GetTotalAssetsCountAsync()
        {

            // Toplam varlık sayısını döndürür
            var allAssets = await _assetRepository.GetAllAsync();
            return allAssets.Count();
        }

        public async Task<decimal> GetTotalCostAllTimeAsync()
        {
            // Tüm zamanlar için toplam maliyeti hesaplar
            var totalCost = await _materialRepository.GetTotalCostAsync(DateTime.MinValue, DateTime.MaxValue);

            return totalCost;
        }

        public async Task<decimal> GetTotalCostForLastYearAsync()
        {
            // Son 365 gün içindeki toplam maliyeti hesaplar
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-365);
            var totalCost = await _materialRepository.GetTotalCostAsync(startDate, endDate);

            return totalCost;
        }

       
    }
}

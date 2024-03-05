using CMMS.Business.Interfaces;
using CMMS.DataAccess.Context;
using CMMS.DataAccess.Repositories;
using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using CMMS.Domain.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<int> GetTotalAssetsCountAsync()
        {
            var totalAssets = await _assetRepository.CountAsync(a => true); // Tüm varlıkların sayısını getir
            return totalAssets;
        }

        public async Task<decimal> GetAssetCountIncreasePercentageAsync()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var currentMonth = DateTime.Today;

            var lastMonthAssetCount = await _assetRepository.CountAsync(a => a.AcceptedDate.Month == lastMonth.Month && a.AcceptedDate.Year == lastMonth.Year);
            var thisMonthAssetCount = await _assetRepository.CountAsync(a => a.AcceptedDate.Month == currentMonth.Month && a.AcceptedDate.Year == currentMonth.Year);

            if (lastMonthAssetCount == 0)
            {
                return thisMonthAssetCount > 0 ? 100 : 0; // Eğer geçen ay hiç varlık eklenmemişse ve bu ay varlık eklenmişse %100 artış, değilse %0 artış var demektir.
            }

            var increase = thisMonthAssetCount - lastMonthAssetCount;
            return (decimal)increase / lastMonthAssetCount * 100; // Yüzdesel artışı hesapla
        }

        public async Task<int> GetActiveUsersCountAsync()
        {
            var activeUsersCount = await _employeeRepository.CountAsync(e => e.IsActive); // Aktif kullanıcıların sayısını getir
            return activeUsersCount;
        }

        public async Task<decimal> GetUserCountIncreasePercentageAsync()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var currentMonth = DateTime.Today;

            var lastMonthUserCount = await _employeeRepository.CountAsync(e => e.DateOfRecruitment.Month == lastMonth.Month && e.DateOfRecruitment.Year == lastMonth.Year);
            var thisMonthUserCount = await _employeeRepository.CountAsync(e => e.DateOfRecruitment.Month == currentMonth.Month && e.DateOfRecruitment.Year == currentMonth.Year);

            if (lastMonthUserCount == 0)
            {
                return thisMonthUserCount > 0 ? 100 : 0; // Eğer geçen ay hiç kullanıcı eklenmemişse ve bu ay kullanıcı eklenmişse %100 artış, değilse %0 artış var demektir.
            }

            var increase = thisMonthUserCount - lastMonthUserCount;
            return (decimal)increase / lastMonthUserCount * 100; // Yüzdesel artışı hesapla
        }


        public async Task<int> GetPlannedTasksCountAsync()
        {
            var plannedTasksCount = await _maintenanceTaskRepository.CountAsync(mt => mt.IsPlanned); // Planlı görevlerin sayısını getir (IsPlanned bir örnek property'dir)
            return plannedTasksCount;
        }

        public async Task<decimal> GetTotalCostForLastYearAsync()
        {
            var oneYearAgo = DateTime.Today.AddYears(-1);
            var totalCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date >= oneYearAgo); // Son 1 yılda yapılan tüm bakım işlemlerinin maliyetini topla
            return totalCost;
        }



        public async Task<decimal> GetTotalCostForLastYearAsync()
        {
            var oneYearAgo = DateTime.Today.AddYears(-1);
            var totalCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date >= oneYearAgo); // Son 1 yılda yapılan tüm bakım işlemlerinin maliyetini topla
            return totalCost;
        }



        public async Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync()
        {
            var recentLogs = await _activityLogRepository.GetAllAsync()
                                .OrderByDescending(log => log.Date)
                                .Take(3)
                                .ToListAsync(); // En son eklenen 3 aktivite logunu getir

            return _mapper.Map<IEnumerable<ActivityLogDto>>(recentLogs);
        }



        public async Task<Dictionary<string, int>> GetAssetTypeDistributionAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var distribution = assets.GroupBy(a => a.Type)
                                     .ToDictionary(g => g.Key.ToString(), g => g.Count()); // Varlık türlerine göre grupla ve her biri için sayım yap

            return distribution;
        }


        public async Task<decimal> GetCostIncreaseFromPreviousMonthAsync()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var currentMonth = DateTime.Today;

            var lastMonthCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date.Month == lastMonth.Month && mt.Date.Year == lastMonth.Year);
            var thisMonthCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date.Month == currentMonth.Month && mt.Date.Year == currentMonth.Year);

            if (lastMonthCost == 0)
            {
                return thisMonthCost > 0 ? 100 : 0; // Eğer geçen ay hiç maliyet yoksa ve bu ay maliyet varsa %100 artış, yoksa %0 artış var demektir.
            }

            var increase = thisMonthCost - lastMonthCost;
            return (decimal)increase / lastMonthCost * 100; // Yüzdesel artışı hesapla
        }

        public async Task<decimal> GetCostIncreaseFromPreviousYearAsync()
        {
            var lastYear = DateTime.Today.AddYears(-1);
            var currentYear = DateTime.Today;

            var lastYearCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date.Year == lastYear.Year);
            var thisYearCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => mt.Date.Year == currentYear.Year);

            if (lastYearCost == 0)
            {
                return thisYearCost > 0 ? 100 : 0; // Eğer geçen yıl hiç maliyet yoksa ve bu yıl maliyet varsa %100 artış, yoksa %0 artış var demektir.
            }

            var increase = thisYearCost - lastYearCost;
            return (decimal)increase / lastYearCost * 100; // Yüzdesel artışı hesapla
        }


        public async Task<decimal> GetTotalCostAllTimeAsync()
        {
            var totalCost = await _maintenanceTaskRepository.GetTotalCostAsync(mt => true); // Tüm zamanlar için toplam maliyeti hesapla
            return totalCost;
        }


        public async Task<Dictionary<string, int>> GetAssetTypeDistributionAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var distribution = assets.GroupBy(a => a.Type)
                                     .ToDictionary(g => g.Key.ToString(), g => g.Count()); // Varlık türlerine göre grupla ve her biri için sayım yap

            return distribution;
        }

    }

}
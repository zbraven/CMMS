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
        private readonly IAssetRepository _assetRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMaintenanceTaskRepository _maintenanceTaskRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        public DashboardService(IAssetRepository assetRepository, IEmployeeRepository employeeRepository, IMaintenanceTaskRepository maintenanceTaskRepository, IMaterialRepository materialRepository, IActivityLogRepository activityLogRepository)
        {
            _assetRepository = assetRepository;
            _employeeRepository = employeeRepository;
            _maintenanceTaskRepository = maintenanceTaskRepository;
            _materialRepository = materialRepository;
            _activityLogRepository = activityLogRepository;
        }

        //Total asset sayısını veren metodum
        public async Task<int> GetTotalAssetsCountAsync()
        {
            return await _assetRepository.CountAsync();
        }

        //Total task sayısını veren metodum
        public async Task<int> GetTotalMaintenanceTaskCountAsync()
        {
            return await _maintenanceTaskRepository.CountAsync();
        }

        //Total kullanıcı sayısını veren metodum
        public async Task<int> GetTotalUserCountAsync()
        {
            return await _employeeRepository.CountAsync();
        }

        //Aktif kullanıcıların sayısını veren metodum
        public async Task<int> GetActiveUsersCountAsync()
        {
            var activeUsers = await _employeeRepository.GetActiveUsersAsync();
            return activeUsers.Count();
        }


        //Asset Tiplerine göre oranlama yapan metodum
        public async Task<IEnumerable<AssetTypeDistributionDto>> GetAssetTypeDistributionAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var distribution = assets.GroupBy(a => a.Type)
                                     .Select(g => new AssetTypeDistributionDto
                                     {
                                         AssetType = g.Key,
                                         Count = g.Count()
                                     }).ToList();

            return distribution;
        }



        public async Task<decimal> GetCostIncreaseFromPreviousMonthAsync()
        {
            // Önceki ayın ilk ve son gününü hesapla
            var lastMonthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
            var lastMonthLastDay = lastMonthFirstDay.AddMonths(1).AddDays(-1);

            // Bu ayın ilk ve son gününü hesapla
            var thisMonthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var thisMonthLastDay = thisMonthFirstDay.AddMonths(1).AddDays(-1);

            // Önceki ayın toplam maliyetini al
            var lastMonthCost = await _materialRepository.GetTotalCostAsync(lastMonthFirstDay, lastMonthLastDay);

            // Bu ayın toplam maliyetini al
            var thisMonthCost = await _materialRepository.GetTotalCostAsync(thisMonthFirstDay, thisMonthLastDay);

            // Eğer önceki ayın maliyeti 0 ise, %100 artış olarak kabul edilir
            if (lastMonthCost == 0)
            {
                return thisMonthCost > 0 ? 100 : 0;
            }

            // Yüzdesel artışı hesapla
            return ((thisMonthCost - lastMonthCost) / lastMonthCost) * 100;
        }


        public async Task<decimal> GetTotalCostAllTimeAsync()
        {
            // Tüm zamanlar için toplam malzeme maliyetini al
            var totalCost = await _materialRepository.GetTotalCostAllTimeAsync();
            return totalCost;
        }


        //Aktif Kullanıcı Oranı Hesap
        public async Task<decimal> GetUserCountIncreasePercentageAsync()
        {
            var lastMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var lastMonthActiveUsers = await _employeeRepository.CountActiveUsersByDateRangeAsync(lastMonthStart, currentMonthStart.AddTicks(-1));
            var thisMonthActiveUsers = await _employeeRepository.CountActiveUsersByDateRangeAsync(currentMonthStart, DateTime.Now);

            if (lastMonthActiveUsers == 0) return thisMonthActiveUsers > 0 ? 100 : 0;

            return ((decimal)(thisMonthActiveUsers - lastMonthActiveUsers) / lastMonthActiveUsers) * 100;
        }


        public async Task<decimal> GetCostIncreaseFromPreviousYearAsync()
        {
            var currentYear = DateTime.Now.Year;
            var previousYear = currentYear - 1;

            var currentYearCost = await _materialRepository.GetTotalCostByYearAsync(currentYear);
            var previousYearCost = await _materialRepository.GetTotalCostByYearAsync(previousYear);

            if (previousYearCost == 0)
            {
                return currentYearCost > 0 ? 100 : 0; // Eğer önceki yılın maliyeti yoksa, %100 artış olarak kabul edilir.
            }

            return ((currentYearCost - previousYearCost) / previousYearCost) * 100;
        }


        public async Task<decimal> GetAssetCountIncreasePercentageAsync()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var lastMonth = DateTime.Now.AddMonths(-1).Month;
            var lastYear = DateTime.Now.AddMonths(-1).Year;

            var lastMonthCount = await _assetRepository.CountAssetsByMonthAsync(lastMonth, lastYear);
            var thisMonthCount = await _assetRepository.CountAssetsByMonthAsync(currentMonth, currentYear);

            if (lastMonthCount == 0)
            {
                return thisMonthCount > 0 ? 100 : 0; // Eğer önceki ay varlık eklenmemişse, %100 artış olarak kabul edilir.
            }

            return ((decimal)(thisMonthCount - lastMonthCount) / lastMonthCount) * 100;
        }

        public async Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync()
        {
            var recentLogs = await _activityLogRepository.GetRecentActivityLogsAsync(3);
            // DTO'ya dönüştürme işlemi (varsa AutoMapper kullanabilirsiniz)
            var recentLogsDto = recentLogs.Select(log => new ActivityLogDto
            {
                Id = log.Id,
                Date = log.Date,
                Notes = log.Notes,
                EmployeeId = log.EmployeeId,
                MaintenanceTaskId = log.MaintenanceTaskId
                // Diğer alanlar...
            });

            return recentLogsDto;
        }

        public async Task<decimal> GetTaskCountIncreasePercentageAsync()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var thisMonth = DateTime.Now;

            var lastMonthTaskCount = await _maintenanceTaskRepository.CountTasksByMonthAsync(lastMonth.Month, lastMonth.Year);
            var thisMonthTaskCount = await _maintenanceTaskRepository.CountTasksByMonthAsync(thisMonth.Month, thisMonth.Year);

            if (lastMonthTaskCount == 0)
            {
                return thisMonthTaskCount > 0 ? 100 : 0; // Eğer geçen ay görev eklenmemişse, %100 artış olarak kabul edilir.
            }

            return ((decimal)(thisMonthTaskCount - lastMonthTaskCount) / lastMonthTaskCount) * 100;
        }
    }

}
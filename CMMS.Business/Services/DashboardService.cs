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
        private readonly CMMSDbContext _context;

        public DashboardService(CMMSDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalAssetsCountAsync()
        {
            return await _context.Assets.CountAsync();
        }

        public async Task<decimal> GetAssetCountIncreasePercentageAsync()
        {
            var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastMonthStart = currentMonthStart.AddMonths(-1);

            var lastMonthCount = await _context.Assets.CountAsync(a => a.AcceptedDate < currentMonthStart && a.AcceptedDate >= lastMonthStart);
            var thisMonthCount = await _context.Assets.CountAsync(a => a.AcceptedDate >= currentMonthStart);

            if (lastMonthCount == 0) return thisMonthCount > 0 ? 100 : 0;

            return ((decimal)(thisMonthCount - lastMonthCount) / lastMonthCount) * 100;
        }


        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _context.Employees.CountAsync(e => e.IsActive);
        }

        public async Task<decimal> GetUserCountIncreasePercentageAsync()
        {
            var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastMonthStart = currentMonthStart.AddMonths(-1);

            var lastMonthActiveUsers = await _context.Employees.CountAsync(e => e.IsActive && e.DateOfRecruitment < currentMonthStart && e.DateOfRecruitment >= lastMonthStart);
            var thisMonthActiveUsers = await _context.Employees.CountAsync(e => e.IsActive && e.DateOfRecruitment >= currentMonthStart);

            if (lastMonthActiveUsers == 0) return thisMonthActiveUsers > 0 ? 100 : 0;

            return ((decimal)(thisMonthActiveUsers - lastMonthActiveUsers) / lastMonthActiveUsers) * 100;
        }

        public async Task<int> GetPlannedTasksCountAsync()
        {
            return await _context.MaintenanceTasks.CountAsync();
        }

        public async Task<decimal> GetTaskCountIncreasePercentageAsync()
        {
            var lastPeriod = DateTime.Now.AddMonths(-1);
            var currentPeriod = DateTime.Now;

            var lastPeriodTaskCount = await _context.MaintenanceTasks.CountAsync(t => t.Date < currentPeriod && t.Date >= lastPeriod);
            var thisPeriodTaskCount = await _context.MaintenanceTasks.CountAsync(t => t.Date >= currentPeriod);

            if (lastPeriodTaskCount == 0) return thisPeriodTaskCount > 0 ? 100 : 0;

            return ((decimal)(thisPeriodTaskCount - lastPeriodTaskCount) / lastPeriodTaskCount) * 100;
        }

        public async Task<decimal> GetTotalCostForLastYearAsync()
        {
            var lastYearStart = new DateTime(DateTime.Now.Year - 1, 1, 1);
            var lastYearEnd = new DateTime(DateTime.Now.Year, 1, 1);

            var lastYearTasks = await _context.MaintenanceTasks
                                .Where(t => t.Date >= lastYearStart && t.Date < lastYearEnd)
                                .Include(t => t.TaskMaterials)
                                .ThenInclude(tm => tm.Material)
                                .ToListAsync();

            return lastYearTasks.Sum(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));
        }

        public async Task<IEnumerable<ActivityLogDto>> GetRecentActivityLogsAsync()
        {
            var recentLogs = await _context.ActivityLogs
                                    .OrderByDescending(log => log.Date)
                                    .Take(10)
                                    .Select(log => new ActivityLogDto
                                    {
                                        Id = log.Id,
                                        Date = log.Date,
                                        Notes = log.Notes,
                                        // Diğer gerekli alanlar
                                    })
                                    .ToListAsync();

            return recentLogs;
        }


        public async Task<Dictionary<string, int>> GetAssetTypeDistributionAsync()
        {
            var distribution = await _context.Assets
                                .GroupBy(a => a.Type)
                                .Select(group => new { Type = group.Key.ToString(), Count = group.Count() })
                                .ToDictionaryAsync(k => k.Type, v => v.Count);

            return distribution;
        }

        public async Task<decimal> GetCostIncreaseFromPreviousMonthAsync()
        {
            // Bu metot, son bir ay içindeki maliyet artışını hesaplar
            var lastMonth = DateTime.Now.AddMonths(-1);
            var thisMonth = DateTime.Now;

            var lastMonthCost = await _context.MaintenanceTasks
                                    .Where(t => t.Date.Month == lastMonth.Month && t.Date.Year == lastMonth.Year)
                                    .Include(t => t.TaskMaterials)
                                    .ThenInclude(tm => tm.Material)
                                    .SumAsync(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));

            var thisMonthCost = await _context.MaintenanceTasks
                                    .Where(t => t.Date.Month == thisMonth.Month && t.Date.Year == thisMonth.Year)
                                    .Include(t => t.TaskMaterials)
                                    .ThenInclude(tm => tm.Material)
                                    .SumAsync(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));

            if (lastMonthCost == 0) return thisMonthCost > 0 ? 100 : 0;

            return ((thisMonthCost - lastMonthCost) / lastMonthCost) * 100;
        }


        public async Task<decimal> GetCostIncreaseFromPreviousYearAsync()
        {
            // Bu metot, son bir yıl içindeki maliyet artışını hesaplar
            var lastYear = DateTime.Now.AddYears(-1);
            var thisYear = DateTime.Now;

            var lastYearCost = await _context.MaintenanceTasks
                                    .Where(t => t.Date.Year == lastYear.Year)
                                    .Include(t => t.TaskMaterials)
                                    .ThenInclude(tm => tm.Material)
                                    .SumAsync(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));

            var thisYearCost = await _context.MaintenanceTasks
                                    .Where(t => t.Date.Year == thisYear.Year)
                                    .Include(t => t.TaskMaterials)
                                    .ThenInclude(tm => tm.Material)
                                    .SumAsync(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));

            if (lastYearCost == 0) return thisYearCost > 0 ? 100 : 0;

            return ((thisYearCost - lastYearCost) / lastYearCost) * 100;
        }


        public async Task<decimal> GetTotalCostAllTimeAsync()
        {
            var totalCost = await _context.MaintenanceTasks
                                .Include(t => t.TaskMaterials)
                                .ThenInclude(tm => tm.Material)
                                .SumAsync(t => t.TaskMaterials.Sum(tm => tm.Quantity * tm.Material.Cost));

            return totalCost;
        }






    }

}
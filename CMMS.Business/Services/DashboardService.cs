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

        private readonly IAssetService _assetService;
        private readonly IEmployeeService _employeeService;
        private readonly IMaintenanceTaskService _maintenanceTaskService;
        private readonly IActivityLogService _activityLogService;


        public async Task<int> GetTotalAssetsCountAsync()
        {
            // Repository üzerinden tüm varlıkların sayısını döndürür.
            return await _assetService.GetTotalAssetsCountAsync();
        }
    }

}
using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<IEnumerable<Plan>> GetPlansByFrequencyAsync(MaintenanceFrequency frequency);
        Task<IEnumerable<Plan>> GetMaintenancePlansByAssetIdAsync(int assetId);

    }
}

using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> SearchMaterialsByNameAsync(string name);
        Task<IEnumerable<Material>> GetMaterialsByCostRangeAsync(decimal minCost, decimal maxCost);
        Task<decimal> GetTotalCostAsync(DateTime startDate, DateTime endDate);

    }

}


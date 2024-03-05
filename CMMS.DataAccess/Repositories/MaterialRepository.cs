using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private readonly CMMSDbContext _context;
        public MaterialRepository(CMMSDbContext context):base(context) 
        {
          _context=context;  
        }

        public async Task<IEnumerable<Material>> GetMaterialsByCostRangeAsync(decimal minCost, decimal maxCost)
        {
            return await _context.Set<Material>().Where(m => m.Cost >= minCost && m.Cost <= maxCost).ToListAsync();
        }

        public async Task<decimal> GetTotalCostAsync(DateTime startDate, DateTime endDate)
        {
            // Belirtilen tarih aralığındaki malzeme giderlerini al
            var totalCost = await _context.Materials
                .Where(m => m.PurchaseDate >= startDate && m.PurchaseDate <= endDate)
                .SumAsync(m => m.Cost);

            return totalCost;
        }

        public async Task<IEnumerable<Material>> SearchMaterialsByNameAsync(string name)
        {
            return await _context.Set<Material>().Where(m => m.Name.Contains(name)).ToListAsync();
        }
    }
}

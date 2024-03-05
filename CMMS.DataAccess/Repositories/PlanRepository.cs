using CMMS.DataAccess.Context;
using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly CMMSDbContext _context;
        public PlanRepository(CMMSDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> CountAsync(Expression<Func<Plan, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Plan>> GetMaintenancePlansByAssetIdAsync(int assetId)
        {
            return await _context.Set<Plan>().Where(x=>x.Assets.Any(y=>y.Id==assetId)).ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByFrequencyAsync(MaintenanceFrequency frequency)
        {
            return await _context.Set<Plan>().Where(x => x.MaintenanceFrequency == frequency).ToListAsync();
        }
    }
}

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
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        private readonly CMMSDbContext _context;
        public ContractRepository(CMMSDbContext context) : base(context)
        {
            _context = context;
        }




        public async Task<IEnumerable<Contract>> GetActiveContractsAsync()
        {
            var now = DateTime.Now;
            return await _context.Set<Contract>().Where(x => x.StartDate <= now && x.EndDate >= now).ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByAssetIdAsync(int assetId)
        {
            return await _context.Set<Contract>().Where(x=>x.AssetId==assetId).ToListAsync();
        }
    }
}

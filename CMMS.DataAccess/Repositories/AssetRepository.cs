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
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly CMMSDbContext _context;
        private readonly IAssetRepository _assetRepository;

        public AssetRepository(CMMSDbContext context, IAssetRepository assetRepository) : base(context)
        {
            _context = context;
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<Asset>> GetAssetsByLocationIdAsync(int locationId)
        {
            return await _context.Set<Asset>().Where(a => a.LocationId == locationId).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetsByPlanIdAsync(int planId)
        {
            return await _context.Set<Asset>().Where(a => a.PlanId == planId).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status)
        {
            return await _context.Set<Asset>().Where(a => a.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetsByTypeAsync(AssetType type)
        {
            return await _context.Set<Asset>().Where(a => a.Type == type).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> SearchAssetsByNameAsync(string name)
        {
            return await _context.Set<Asset>().Where(a => a.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _assetRepository.CountAsync();
        }


    }

}

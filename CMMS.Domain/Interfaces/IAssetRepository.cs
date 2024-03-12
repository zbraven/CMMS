using CMMS.Domain.Entities;
using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAssetsByTypeAsync(AssetType type);
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);
        Task<IEnumerable<Asset>> GetAssetsByLocationIdAsync(int locationId);
        Task<IEnumerable<Asset>> GetAssetsByPlanIdAsync(int planId);
        Task<IEnumerable<Asset>> SearchAssetsByNameAsync(string name);

        Task<int> CountAssetsByMonthAsync(int month, int year);


    }
}


using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetDto>> GetAllAssetsAsync();
        Task<AssetDto> GetAssetByIdAsync(int assetId);
        Task<AssetDto> CreateAssetAsync(AssetDto assetDto);
        Task UpdateAssetAsync(AssetDto assetDto);
        Task DeleteAssetAsync(int assetId);

        Task<int> GetTotalAssetsCountAsync();
    }
}

using AutoMapper;
using CMMS.Business.Interfaces;
using CMMS.Domain.Entities;
using CMMS.Domain.Interfaces;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        public async Task<AssetDto> GetAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<AssetDto> CreateAssetAsync(AssetDto AssetDto)
        {
            var asset = _mapper.Map<Asset>(AssetDto);
            await _assetRepository.AddAsync(asset);
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task UpdateAssetAsync(AssetDto AssetDto)
        {
            var asset = _mapper.Map<Asset>(AssetDto);
            await _assetRepository.UpdateAsync(asset);
        }

        public async Task DeleteAssetAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset != null)
            {
                await _assetRepository.DeleteAsync(asset);
            }
        }
    }
}

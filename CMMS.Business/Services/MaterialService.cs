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
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialDto>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MaterialDto>>(materials);
        }

        public async Task<MaterialDto> GetMaterialByIdAsync(int materialId)
        {
            var material = await _materialRepository.GetByIdAsync(materialId);
            return _mapper.Map<MaterialDto>(material);
        }

        public async Task<MaterialDto> CreateMaterialAsync(MaterialDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            await _materialRepository.AddAsync(material);
            return _mapper.Map<MaterialDto>(material);
        }

        public async Task UpdateMaterialAsync(MaterialDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            await _materialRepository.UpdateAsync(material);
        }

        public async Task DeleteMaterialAsync(int materialId)
        {
            var material = await _materialRepository.GetByIdAsync(materialId);
            if (material != null)
            {
                await _materialRepository.DeleteAsync(material);
            }
        }
    }
}

using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDto>> GetAllMaterialsAsync();
        Task<MaterialDto> GetMaterialByIdAsync(int materialId);
        Task<MaterialDto> CreateMaterialAsync(MaterialDto materialDto);
        Task UpdateMaterialAsync(MaterialDto materialDto);
        Task DeleteMaterialAsync(int materialId);
    }
}

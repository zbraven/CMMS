using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<PlanDto> GetPlanByIdAsync(int planId);
        Task<PlanDto> CreatePlanAsync(PlanDto planDto);
        Task UpdatePlanAsync(PlanDto planDto);
        Task DeletePlanAsync(int planId);
    }
}

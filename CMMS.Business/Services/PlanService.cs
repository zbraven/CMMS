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
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public PlanService(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }

        public async Task<PlanDto> GetPlanByIdAsync(int planId)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            return _mapper.Map<PlanDto>(plan);
        }

        public async Task<PlanDto> CreatePlanAsync(PlanDto planDto)
        {
            var plan = _mapper.Map<Plan>(planDto);
            await _planRepository.AddAsync(plan);
            return _mapper.Map<PlanDto>(plan);
        }

        public async Task UpdatePlanAsync(PlanDto planDto)
        {
            var plan = _mapper.Map<Plan>(planDto);
            await _planRepository.UpdateAsync(plan);
        }

        public async Task DeletePlanAsync(int planId)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            if (plan != null)
            {
                await _planRepository.DeleteAsync(plan);
            }
        }
    }
}

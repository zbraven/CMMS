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
    public class TaskMaterialService : ITaskMaterialService
    {
        private readonly ITaskMaterialRepository _taskMaterialRepository;
        private readonly IMapper _mapper;

        public TaskMaterialService(ITaskMaterialRepository taskMaterialRepository, IMapper mapper)
        {
            _taskMaterialRepository = taskMaterialRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskMaterialDto>> GetAllTaskMaterialsAsync()
        {
            var taskMaterials = await _taskMaterialRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskMaterialDto>>(taskMaterials);
        }

        public async Task<TaskMaterialDto> GetTaskMaterialByIdAsync(int taskMaterialId)
        {
            var taskMaterial = await _taskMaterialRepository.GetByIdAsync(taskMaterialId);
            return _mapper.Map<TaskMaterialDto>(taskMaterial);
        }

        public async Task<TaskMaterialDto> CreateTaskMaterialAsync(TaskMaterialDto taskMaterialDto)
        {
            var taskMaterial = _mapper.Map<TaskMaterial>(taskMaterialDto);
            await _taskMaterialRepository.AddAsync(taskMaterial);
            return _mapper.Map<TaskMaterialDto>(taskMaterial);
        }

        public async Task UpdateTaskMaterialAsync(TaskMaterialDto taskMaterialDto)
        {
            var taskMaterial = _mapper.Map<TaskMaterial>(taskMaterialDto);
            await _taskMaterialRepository.UpdateAsync(taskMaterial);
        }

        public async Task DeleteTaskMaterialAsync(int taskMaterialId)
        {
            var taskMaterial = await _taskMaterialRepository.GetByIdAsync(taskMaterialId);
            if (taskMaterial != null)
            {
                await _taskMaterialRepository.DeleteAsync(taskMaterial);
            }
        }
    }
}

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
    public class MaintenanceTaskService : IMaintenanceTaskService
    {
        private readonly IMaintenanceTaskRepository _maintenanceTaskRepository;
        private readonly IMapper _mapper;

        public MaintenanceTaskService(IMaintenanceTaskRepository maintenanceTaskRepository, IMapper mapper)
        {
            _maintenanceTaskRepository = maintenanceTaskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaintenanceTaskDto>> GetAllMaintenanceTasksAsync()
        {
            var tasks = await _maintenanceTaskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MaintenanceTaskDto>>(tasks);
        }

        public async Task<MaintenanceTaskDto> GetMaintenanceTaskByIdAsync(int taskId)
        {
            var task = await _maintenanceTaskRepository.GetByIdAsync(taskId);
            return _mapper.Map<MaintenanceTaskDto>(task);
        }

        public async Task<MaintenanceTaskDto> CreateMaintenanceTaskAsync(MaintenanceTaskDto taskDto)
        {
            var task = _mapper.Map<MaintenanceTask>(taskDto);
            await _maintenanceTaskRepository.AddAsync(task);
            return _mapper.Map<MaintenanceTaskDto>(task);
        }

        public async Task UpdateMaintenanceTaskAsync(MaintenanceTaskDto taskDto)
        {
            var task = _mapper.Map<MaintenanceTask>(taskDto);
            await _maintenanceTaskRepository.UpdateAsync(task);
        }

        public async Task DeleteMaintenanceTaskAsync(int taskId)
        {
            var task = await _maintenanceTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                await _maintenanceTaskRepository.DeleteAsync(task);
            }
        }
    }
}

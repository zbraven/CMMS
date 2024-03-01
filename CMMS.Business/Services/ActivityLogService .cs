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
    public class ActivityLogService : IActivityLogService
    {
        private readonly IRepository<ActivityLog> _logRepository;
        private readonly IMapper _mapper;

        public ActivityLogService(IRepository<ActivityLog> logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityLogDto>> GetAllActivityLogsAsync()
        {
            var logs = await _logRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityLogDto>>(logs);
        }

        public async Task<ActivityLogDto> GetActivityLogByIdAsync(int logId)
        {
            var log = await _logRepository.GetByIdAsync(logId);
            return _mapper.Map<ActivityLogDto>(log);
        }

        public async Task<ActivityLogDto> CreateActivityLogAsync(ActivityLogDto logDto)
        {
            var log = _mapper.Map<ActivityLog>(logDto);
            await _logRepository.AddAsync(log);
            return _mapper.Map<ActivityLogDto>(log);
        }

        public async Task UpdateActivityLogAsync(ActivityLogDto logDto)
        {
            var log = _mapper.Map<ActivityLog>(logDto);
            await _logRepository.UpdateAsync(log);
        }

        public async Task DeleteActivityLogAsync(int logId)
        {
            var log = await _logRepository.GetByIdAsync(logId);
            if (log != null)
            {
                await _logRepository.DeleteAsync(log);
            }
        }
    }
}

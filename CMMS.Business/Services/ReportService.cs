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
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
        {
            var reports = await _reportRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }

        public async Task<ReportDto> GetReportByIdAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            return _mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> CreateReportAsync(ReportDto reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);
            await _reportRepository.AddAsync(report);
            return _mapper.Map<ReportDto>(report);
        }

        public async Task UpdateReportAsync(ReportDto reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);
            await _reportRepository.UpdateAsync(report);
        }

        public async Task DeleteReportAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report != null)
            {
                await _reportRepository.DeleteAsync(report);
            }
        }
    }
}

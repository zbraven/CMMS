using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllReportsAsync();
        Task<ReportDto> GetReportByIdAsync(int reportId);
        Task<ReportDto> CreateReportAsync(ReportDto reportDto);
        Task UpdateReportAsync(ReportDto reportDto);
        Task DeleteReportAsync(int reportId);
    }
}

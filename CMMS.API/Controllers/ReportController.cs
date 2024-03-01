using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // Tüm Report kayıtlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        // Belirli bir ID'ye sahip Report kaydını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        // Yeni bir Report kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdReport = await _reportService.CreateReportAsync(reportDto);
            return CreatedAtAction(nameof(GetReportById), new { id = createdReport.Id }, createdReport);
        }

        // Var olan bir Report kaydını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingReport = await _reportService.GetReportByIdAsync(id);
            if (existingReport == null)
            {
                return NotFound();
            }

            await _reportService.UpdateReportAsync(reportDto);
            return NoContent();
        }

        // Belirli bir ID'ye sahip Report kaydını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var reportToDelete = await _reportService.GetReportByIdAsync(id);
            if (reportToDelete == null)
            {
                return NotFound();
            }

            await _reportService.DeleteReportAsync(id);
            return NoContent();
        }
    }
}


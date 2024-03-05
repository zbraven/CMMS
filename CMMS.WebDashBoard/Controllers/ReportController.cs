using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: Report
        public async Task<IActionResult> Index()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return View(reports);
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // Additional actions can be implemented here, such as for creating or generating new reports,
        // if your application requires such functionalities.
    }
}

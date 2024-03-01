using CMMS.Business.Interfaces;
using CMMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService _assetService;

        public HomeController(ILogger<HomeController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _assetService.GetAllAssetsAsync();
            return View(datas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

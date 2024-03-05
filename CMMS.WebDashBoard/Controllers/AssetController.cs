using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        // Tüm Asset kayıtlarını getirir ve index view'ına gönderir
        public async Task<IActionResult> Index()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return View(assets);
        }

        // Belirli bir ID'ye sahip Asset kaydının detaylarını getirir ve details view'ına gönderir
        public async Task<IActionResult> Details(int id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        // Yeni Asset oluşturma formunu gösterir
        public IActionResult Create()
        {
            return View();
        }

        // Yeni bir Asset kaydı ekler ve index view'ına yönlendirir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SerialNumber,...")] AssetDto assetDto)
        {
            if (ModelState.IsValid)
            {
                await _assetService.CreateAssetAsync(assetDto);
                return RedirectToAction(nameof(Index));
            }
            return View(assetDto);
        }

        // Asset düzenleme formunu gösterir
        public async Task<IActionResult> Edit(int id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        // Var olan bir Asset kaydını günceller ve index view'ına yönlendirir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SerialNumber,...")] AssetDto assetDto)
        {
            if (id != assetDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assetService.UpdateAssetAsync(assetDto);
                return RedirectToAction(nameof(Index));
            }
            return View(assetDto);
        }

        // Asset silme onay formunu gösterir
        public async Task<IActionResult> Delete(int id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        // Belirli bir ID'ye sahip Asset kaydını siler ve index view'ına yönlendirir
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _assetService.DeleteAssetAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using CMMS.Business.Interfaces;
using CMMS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMMS.Web.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // Tüm Material kayıtlarını getirir ve index view'ına gönderir
        public async Task<IActionResult> Index()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return View(materials);
        }

        // Belirli bir ID'ye sahip Material kaydının detaylarını getirir ve details view'ına gönderir
        public async Task<IActionResult> Details(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // Yeni Material oluşturma formunu gösterir
        public IActionResult Create()
        {
            return View();
        }

        // Yeni bir Material kaydı ekler ve index view'ına yönlendirir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Cost,...")] MaterialDto materialDto)
        {
            if (ModelState.IsValid)
            {
                await _materialService.CreateMaterialAsync(materialDto);
                return RedirectToAction(nameof(Index));
            }
            return View(materialDto);
        }

        // Material düzenleme formunu gösterir
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // Var olan bir Material kaydını günceller ve index view'ına yönlendirir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost,...")] MaterialDto materialDto)
        {
            if (id != materialDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _materialService.UpdateMaterialAsync(materialDto);
                return RedirectToAction(nameof(Index));
            }
            return View(materialDto);
        }

        // Material silme onay formunu gösterir
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // Belirli bir ID'ye sahip Material kaydını siler ve index view'ına yönlendirir
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _materialService.DeleteMaterialAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using HediyelikEsya.Data;
using HediyelikEsya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HediyelikEsya.Controllers
{
    // [Authorize] etiketi sınıfın en üstündeyken, altındaki TÜM metodlar şifreye bağlanır.
    [Authorize] 
    public class UrunlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME (Giriş yapmayanların görmesi için [AllowAnonymous] ekledik)
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var urunler = await _context.Urunler.ToListAsync();
            return View(urunler);
        }

        // 2. DETAY SAYFASI (Giriş yapmayanların görmesi için [AllowAnonymous] ekledik)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var urun = await _context.Urunler
                .FirstOrDefaultAsync(m => m.Id == id);

            if (urun == null) return NotFound();

            return View(urun);
        }

        // --- DİĞER TÜM METODLAR (Create, Edit, Delete) OTOMATİK OLARAK KİLİTLİDİR ---

        // 3. EKLEME
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // 4. GÜNCELLEME
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null) return NotFound();

            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Urun urun)
        {
            if (id != urun.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Urunler.Any(e => e.Id == urun.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // 5. SİLME
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var urun = await _context.Urunler.FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null) return NotFound();

            return View(urun);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                _context.Urunler.Remove(urun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
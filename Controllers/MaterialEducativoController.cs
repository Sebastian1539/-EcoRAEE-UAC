using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Data;
using EcoRAEE_UAC.Models;

namespace EcoRAEE_UAC.Controllers
{
    public class MaterialEducativoController : Controller
    {
        private readonly AppDbContext _context;

        public MaterialEducativoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var materiales = await _context.MaterialesEducativos
                .Include(m => m.Campaña)
                .OrderByDescending(m => m.FechaPublicacion)
                .ToListAsync();
            return View(materiales);
        }

        public IActionResult Create()
        {
            ViewBag.Campañas = _context.Campañas.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Titulo,Tipo,Descripcion,UrlArchivo,FechaPublicacion,CampañaId")] MaterialEducativo material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Material educativo publicado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(material);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var material = await _context.MaterialesEducativos.FindAsync(id);
            if (material == null) return NotFound();
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Titulo,Tipo,Descripcion,UrlArchivo,FechaPublicacion,CampañaId")] MaterialEducativo material)
        {
            if (id != material.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "Material educativo actualizado.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MaterialesEducativos.Any(e => e.Id == material.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(material);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var material = await _context.MaterialesEducativos
                .Include(m => m.Campaña)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null) return NotFound();
            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.MaterialesEducativos.FindAsync(id);
            if (material != null)
            {
                _context.MaterialesEducativos.Remove(material);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Material eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

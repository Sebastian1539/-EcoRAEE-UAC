using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Data;
using EcoRAEE_UAC.Models;

namespace EcoRAEE_UAC.Controllers
{
    public class RecoleccionRaeeController : Controller
    {
        private readonly AppDbContext _context;

        public RecoleccionRaeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recolecciones = await _context.RecoleccionesRaee
                .Include(r => r.Campaña)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
            return View(recolecciones);
        }

        public IActionResult Create()
        {
            ViewBag.Campañas = _context.Campañas.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("TipoResiduo,CantidadKg,LugarRecoleccion,Fecha,CampañaId,Observaciones")] RecoleccionRaee recoleccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recoleccion);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Recolección RAEE registrada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(recoleccion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var recoleccion = await _context.RecoleccionesRaee.FindAsync(id);
            if (recoleccion == null) return NotFound();
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(recoleccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,TipoResiduo,CantidadKg,LugarRecoleccion,Fecha,CampañaId,Observaciones")] RecoleccionRaee recoleccion)
        {
            if (id != recoleccion.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recoleccion);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "Recolección actualizada exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.RecoleccionesRaee.Any(e => e.Id == recoleccion.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(recoleccion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var recoleccion = await _context.RecoleccionesRaee
                .Include(r => r.Campaña)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recoleccion == null) return NotFound();
            return View(recoleccion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recoleccion = await _context.RecoleccionesRaee.FindAsync(id);
            if (recoleccion != null)
            {
                _context.RecoleccionesRaee.Remove(recoleccion);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Recolección eliminada.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

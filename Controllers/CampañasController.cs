using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Data;
using EcoRAEE_UAC.Models;

namespace EcoRAEE_UAC.Controllers
{
    public class CampañasController : Controller
    {
        private readonly AppDbContext _context;

        public CampañasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var campañas = await _context.Campañas
                .Include(c => c.Participantes)
                .Include(c => c.RecoleccionesRaee)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
            return View(campañas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var campaña = await _context.Campañas
                .Include(c => c.Participantes)
                .Include(c => c.RecoleccionesRaee)
                .Include(c => c.Materiales)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (campaña == null) return NotFound();
            return View(campaña);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nombre,Fecha,Lugar,Responsable,Descripcion,Estado")] Campaña campaña)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaña);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Campaña registrada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(campaña);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var campaña = await _context.Campañas.FindAsync(id);
            if (campaña == null) return NotFound();
            return View(campaña);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Nombre,Fecha,Lugar,Responsable,Descripcion,Estado")] Campaña campaña)
        {
            if (id != campaña.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var campañaExistente = await _context.Campañas.FindAsync(id);
                if (campañaExistente == null) return NotFound();

                campañaExistente.Nombre = campaña.Nombre;
                campañaExistente.Fecha = campaña.Fecha;
                campañaExistente.Lugar = campaña.Lugar;
                campañaExistente.Responsable = campaña.Responsable;
                campañaExistente.Descripcion = campaña.Descripcion;
                campañaExistente.Estado = campaña.Estado;

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "Campaña actualizada exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Campañas.Any(e => e.Id == campaña.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campaña);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var campaña = await _context.Campañas.FirstOrDefaultAsync(m => m.Id == id);
            if (campaña == null) return NotFound();
            return View(campaña);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaña = await _context.Campañas.FindAsync(id);
            if (campaña != null)
            {
                _context.Campañas.Remove(campaña);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Campaña eliminada.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

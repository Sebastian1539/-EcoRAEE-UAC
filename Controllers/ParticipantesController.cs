using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Data;
using EcoRAEE_UAC.Models;

namespace EcoRAEE_UAC.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly AppDbContext _context;

        public ParticipantesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var participantes = await _context.Participantes
                .Include(p => p.Campaña)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();
            return View(participantes);
        }

        public IActionResult Create()
        {
            ViewBag.Campañas = _context.Campañas.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nombres,Apellidos,TipoParticipante,DNI,Email,Telefono,CampañaId")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participante);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Participante registrado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(participante);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null) return NotFound();
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(participante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Nombres,Apellidos,TipoParticipante,DNI,Email,Telefono,CampañaId")] Participante participante)
        {
            if (id != participante.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "Participante actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Participantes.Any(e => e.Id == participante.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campañas = _context.Campañas.ToList();
            return View(participante);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var participante = await _context.Participantes
                .Include(p => p.Campaña)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participante == null) return NotFound();
            return View(participante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante != null)
            {
                _context.Participantes.Remove(participante);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Participante eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

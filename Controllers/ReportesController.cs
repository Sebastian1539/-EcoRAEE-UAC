using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Data;
using EcoRAEE_UAC.Models;
using EcoRAEE_UAC.Models.ViewModels;

namespace EcoRAEE_UAC.Controllers
{
    public class ReportesController : Controller
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var campañas = await _context.Campañas
                .Include(c => c.Participantes)
                .Include(c => c.RecoleccionesRaee)
                .ToListAsync();

            var viewModel = new ReporteViewModel
            {
                TotalCampañas = campañas.Count,
                TotalParticipantes = campañas.Sum(c => c.Participantes.Count),
                TotalRaeeKg = campañas.Sum(c => c.RecoleccionesRaee.Sum(r => r.CantidadKg)),
                CampañasFinalizadas = campañas.Count(c => c.Estado == EstadoCampaña.Finalizada),
                ParticipantesPorTipo = await _context.Participantes
                    .GroupBy(p => p.TipoParticipante)
                    .Select(g => new { Tipo = g.Key.ToString(), Total = g.Count() })
                    .ToDictionaryAsync(x => x.Tipo, x => x.Total),
                RaeePorTipo = await _context.RecoleccionesRaee
                    .GroupBy(r => r.TipoResiduo)
                    .Select(g => new { Tipo = g.Key, TotalKg = g.Sum(r => r.CantidadKg) })
                    .ToDictionaryAsync(x => x.Tipo, x => x.TotalKg),
            };

            return View(viewModel);
        }
    }
}

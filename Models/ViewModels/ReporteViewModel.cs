namespace EcoRAEE_UAC.Models.ViewModels
{
    public class ReporteViewModel
    {
        public int TotalCampañas { get; set; }
        public int TotalParticipantes { get; set; }
        public decimal TotalRaeeKg { get; set; }
        public int CampañasFinalizadas { get; set; }
        public Dictionary<string, int> ParticipantesPorTipo { get; set; } = new();
        public Dictionary<string, decimal> RaeePorTipo { get; set; } = new();
    }
}

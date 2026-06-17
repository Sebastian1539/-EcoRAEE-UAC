using System.ComponentModel.DataAnnotations;

namespace EcoRAEE_UAC.Models
{
    public class Campaña
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Nombre de Campaña")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El lugar es obligatorio")]
        [StringLength(200)]
        public string Lugar { get; set; } = string.Empty;

        [Required(ErrorMessage = "El responsable es obligatorio")]
        [StringLength(150)]
        public string Responsable { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; }

        public EstadoCampaña Estado { get; set; } = EstadoCampaña.Planificada;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
        public ICollection<RecoleccionRaee> RecoleccionesRaee { get; set; } = new List<RecoleccionRaee>();
        public ICollection<MaterialEducativo> Materiales { get; set; } = new List<MaterialEducativo>();
    }

    public enum EstadoCampaña
    {
        Planificada,
        [Display(Name = "En Curso")] EnCurso,
        Finalizada
    }
}

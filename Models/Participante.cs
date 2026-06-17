using System.ComponentModel.DataAnnotations;

namespace EcoRAEE_UAC.Models
{
    public class Participante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(150)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(150)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tipo de Participante")]
        public TipoParticipante TipoParticipante { get; set; }

        [StringLength(20)]
        public string? DNI { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Phone]
        public string? Telefono { get; set; }

        [Required]
        [Display(Name = "Campaña")]
        public int CampañaId { get; set; }
        public Campaña? Campaña { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }

    public enum TipoParticipante { Estudiante, Docente, Ciudadano }
}

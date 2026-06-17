using System.ComponentModel.DataAnnotations;

namespace EcoRAEE_UAC.Models
{
    public class RecoleccionRaee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de residuo es obligatorio")]
        [StringLength(150)]
        [Display(Name = "Tipo de Residuo")]
        public string TipoResiduo { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99, ErrorMessage = "La cantidad debe ser mayor a cero")]
        [Display(Name = "Cantidad (Kg)")]
        public decimal CantidadKg { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Lugar de Recolección")]
        public string LugarRecoleccion { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Campaña")]
        public int CampañaId { get; set; }
        public Campaña? Campaña { get; set; }

        public string? Observaciones { get; set; }
    }
}

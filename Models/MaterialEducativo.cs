using System.ComponentModel.DataAnnotations;

namespace EcoRAEE_UAC.Models
{
    public class MaterialEducativo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tipo de Material")]
        public TipoMaterial Tipo { get; set; }

        public string? Descripcion { get; set; }

        [StringLength(500)]
        [Display(Name = "URL / Archivo")]
        public string? UrlArchivo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Publicación")]
        public DateTime FechaPublicacion { get; set; }

        public int? CampañaId { get; set; }
        public Campaña? Campaña { get; set; }
    }

    public enum TipoMaterial
    {
        Infografia,
        Video,
        PDF,
        Noticia
    }
}

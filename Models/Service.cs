using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Models;

public class Service
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[\p{L}\d\s'’-]+$", ErrorMessage = "Nombre inválido")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo es obligatorio")]
    [StringLength(30)]
    public string Type { get; set; } = "GENERAL"; // p.ej. GENERAL, MANTENIMIENTO, REPARACION

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 999999.99, ErrorMessage = "Precio inválido")]
    public decimal Price { get; set; }

    [StringLength(300)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Domain.Entities;

public class Service
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
    [RegularExpression(@"^[\p{L}\s0-9'-]+$",
        ErrorMessage = "El nombre solo puede tener letras, números, espacios, apóstrofes y guiones")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de servicio es obligatorio")]
    [RegularExpression(@"^[\p{L}\s'-]+$",
        ErrorMessage = "El tipo solo puede tener letras, espacios, apóstrofes y guiones")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe superar 500 caracteres")]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario que modificó debe ser válido")]
    public int? ModifiedByUserId { get; set; }
}

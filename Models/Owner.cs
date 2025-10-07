using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Models;

public class Owner
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[\p{L}\s'-]+$",
        ErrorMessage = "El nombre solo puede tener letras, espacios, apóstrofes y guiones")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido paterno es obligatorio")]
    [RegularExpression(@"^[\p{L}\s'-]+$",
        ErrorMessage = "El apellido solo puede tener letras, espacios, apóstrofes y guiones")]
    public string FirstLastname { get; set; } = string.Empty;

    [RegularExpression(@"^[\p{L}\s'-]*$",
        ErrorMessage = "El apellido solo puede tener letras, espacios, apóstrofes y guiones")]
    public string? SecondLastname { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^\+591\d{7,8}$",
        ErrorMessage = "El teléfono debe empezar con +591 y tener 7 u 8 dígitos")]
    public string PhoneNumber { get; set; } = string.Empty;


    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo debe tener un formato válido (incluye @ y .)")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El CI es obligatorio")]
    [StringLength(14, MinimumLength = 5, ErrorMessage = "El CI debe tener entre 5 y 14 caracteres")]
    public string Ci { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La dirección no debe superar 200 caracteres")]
    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[\p{L}\s'-]+$",
        ErrorMessage = "El nombre solo puede tener letras, espacios, apóstrofes y guiones")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido paterno es obligatorio")]
    [RegularExpression(@"^[\p{L}\s'-]+$",
        ErrorMessage = "El apellido solo puede tener letras, espacios, apóstrofes y guiones")]
    public string FirstLastName { get; set; } = string.Empty;

    [RegularExpression(@"^[\p{L}\s'-]*$",
        ErrorMessage = "El apellido solo puede tener letras, espacios, apóstrofes y guiones")]
    public string? SecondLastName { get; set; }

    [Required(ErrorMessage = "El CI es obligatorio")]
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Numero de CI invalido")]
    public string Ci { get; set; } = string.Empty;
    /// Se genera por si solo con: PrimeraLetraNombre.PrimerApellido.Ultimos3DigitosCI
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, ErrorMessage = "La contraseña no debe superar 100 caracteres")]
    public string Password { get; set; } = string.Empty;
    /// Valor predeterminado: "Administrador"
    /// Otros posibles: "PropietarioTaller", "Empleado"
    [Required(ErrorMessage = "El rol es obligatorio")]
    public string Role { get; set; } = "Empleado";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int? ModifiedByUserId { get; set; }
}
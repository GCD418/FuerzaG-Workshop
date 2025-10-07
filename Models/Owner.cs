namespace FuerzaG.Models;

public class Owner
{
    public short Id { get; set; }                        // Primary Key
    public string Name { get; set; } = string.Empty;     // Nombres
    public string FirstLastname { get; set; } = string.Empty; // Primer apellido
    public string? SecondLastname { get; set; }           // Segundo apellido (opcional)
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Ci { get; set; } = string.Empty;        // Cédula o carnet
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
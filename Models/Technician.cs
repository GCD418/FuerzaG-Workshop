namespace FuerzaG.Models;

public class Technician
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FirstLastname { get; set; } = string.Empty;
    public string? SecondLastname { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Ci { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
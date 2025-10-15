using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Domain.Entities;

public class Owner
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FirstLastname { get; set; } = string.Empty;

    public string? SecondLastname { get; set; }

    public int PhoneNumber { get; set; }


    public string Email { get; set; } = string.Empty;

    public string Ci { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

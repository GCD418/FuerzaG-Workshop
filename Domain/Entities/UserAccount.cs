using System.ComponentModel.DataAnnotations;

namespace FuerzaG.Domain.Entities;

public class UserAccount
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string FirstLastName { get; set; } = string.Empty;

    public string? SecondLastName { get; set; }

    public int? PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(14)]
    public string DocumentNumber { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public int? ModifiedByUserId { get; set; }
}
namespace FuerzaG.Models
{
    public class Technician
    {
        public short Id { get; set; }                          // Primary Key
        public string Names { get; set; } = string.Empty;      // Nombres
        public string FirstLastname { get; set; } = string.Empty;
        public string? SecondLastname { get; set; }            // Opcional
        public string Speciality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

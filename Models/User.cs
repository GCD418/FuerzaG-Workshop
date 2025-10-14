namespace FuerzaG.Models
{
    public class User
    {
        public int Id { get; set; }                              
        public string Name { get; set; } = string.Empty;
        public string FirstLastName { get; set; } = string.Empty;
        public string? SecondLastName { get; set; }
        public string Ci { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }                   
        public DateTime? UpdatedAt { get; set; }                  
        public bool IsActive { get; set; } = true;                
        public int? ModifiedByUserId { get; set; }                
    }
}
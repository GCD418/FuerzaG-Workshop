namespace FuerzaG.Models
{
    public class Service
    {
        public short Id { get; set; }                        
        public string Name { get; set; } = string.Empty;     
        public string Type { get; set; } = string.Empty;     
        public decimal Price { get; set; }                   
        public string Description { get; set; } = string.Empty; 
        public bool IsActive { get; set; } = true;           
    }
}

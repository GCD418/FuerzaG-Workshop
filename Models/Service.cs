namespace FuerzaG.Models
{
    public class Service
    {
        public int Id { get; set; }                              // Primary Key
        public string Name { get; set; } = string.Empty;          // Nombre del servicio
        public string Type { get; set; } = string.Empty;          // Tipo de servicio (varchar(12))
        public decimal Price { get; set; }                        // Precio del servicio (decimal(10,2))
        public string Description { get; set; } = string.Empty;   // Descripción del servicio

        public DateTime CreatedAt { get; set; }                   // Fecha de creación
        public DateTime? UpdatedAt { get; set; }                  // Fecha de última actualización
        public bool IsActive { get; set; } = true;                // Estado del servicio (activo o inactivo)
        public int? ModifiedByUserId { get; set; }                // ID del último admin que modificó
    }
}
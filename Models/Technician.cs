using System;

namespace FuerzaG.Models
{
    public class Technician
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? First_Last_Name { get; set; }
        public string? Second_Last_Name { get; set; }
        public string? Phone_Number { get; set; }
        public string? Email { get; set; }
        public string? Document_Number { get; set; }
        public string? Address { get; set; }
        public decimal? Base_Salary { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public bool Is_Active { get; set; } = true;
        public int? Modified_By_User_Id { get; set; }
    }
}

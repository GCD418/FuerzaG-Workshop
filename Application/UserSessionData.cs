namespace FuerzaG.Application;

public class UserSessionData
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
        
    public bool IsCEO => Role == "CEO";
    public bool IsManager => Role == "Manager";
}
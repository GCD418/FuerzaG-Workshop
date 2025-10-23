namespace FuerzaG.Infrastructure.Security;

public interface ICurrentUser
{
    /// <summary>
    /// Id del usuario autenticado (null si no hay sesión)
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// Indica si el usuario está autenticado
    /// </summary>
    bool IsAuthenticated { get; }
}

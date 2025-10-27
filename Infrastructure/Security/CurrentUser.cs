using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FuerzaG.Infrastructure.Security;

/// <summary>
/// Permite acceder al usuario actual autenticado desde HttpContext
/// </summary>
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Indica si hay un usuario autenticado
    /// </summary>
    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

    /// <summary>
    /// Devuelve el ID del usuario autenticado
    /// </summary>
    public int? UserId
    {
        get
        {
            // Busca el Claim que contiene el ID del usuario (usualmente ClaimTypes.NameIdentifier o "sub")
            var idClaim =
                _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            if (int.TryParse(idClaim, out var userId))
                return userId;

            return null;
        }
    }
}

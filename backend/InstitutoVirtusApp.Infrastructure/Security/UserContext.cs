using System.Security.Claims;
using InstitutoVirtusApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InstitutoVirtusApp.Infrastructure.Security;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? Email =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    public string? Name =>
        _httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value;
}

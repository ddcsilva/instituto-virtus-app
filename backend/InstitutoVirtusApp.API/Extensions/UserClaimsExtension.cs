using System.Security.Claims;

namespace InstitutoVirtusApp.API.Extensions;

public static class UserClaimsExtension
{
    public static string? GetUid(this ClaimsPrincipal user)
    {
        return user?.FindFirstValue("user_id");
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user?.FindFirstValue(ClaimTypes.Email);
    }

    public static string? GetName(this ClaimsPrincipal user)
    {
        return user?.FindFirstValue(ClaimTypes.Name);
    }

    public static bool HasEmail(this ClaimsPrincipal user, string email)
    {
        return string.Equals(user?.GetEmail(), email, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsAuthenticated(this ClaimsPrincipal user)
    {
        return user?.Identity?.IsAuthenticated ?? false;
    }
}

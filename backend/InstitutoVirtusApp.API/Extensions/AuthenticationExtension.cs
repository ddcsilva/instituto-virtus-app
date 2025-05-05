using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InstitutoVirtusApp.API.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/instituto-virtus-app";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/instituto-virtus-app",
                    ValidateAudience = true,
                    ValidAudience = "instituto-virtus-app",
                    ValidateLifetime = true
                };
            });

        return services;
    }
}

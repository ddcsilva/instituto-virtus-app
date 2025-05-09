using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Repositories;

namespace Virtus.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurarCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });

    public static void ConfigurarDependencias(this IServiceCollection services)
    {
        services.AddScoped<CriarAlunoHandler>();
    }

    public static void ConfigurarSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void ConfigurarControllers(this IServiceCollection services)
    {
        services.AddControllers();
    }
}

using Microsoft.EntityFrameworkCore;
using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Repositories;
using Virtus.Infra.Data;
using Virtus.Infra.Repositories;

namespace Virtus.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurarCors(this IServiceCollection services)
    {
        services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
                });
    }

    public static void ConfigurarDbContext(this IServiceCollection services)
    {
        services.AddDbContext<VirtusDbContext>(options =>
            options.UseSqlite("DefaultConnection"));
    }

    public static void ConfigurarDependencias(this IServiceCollection services)
    {
        services.AddScoped<CriarAlunoHandler>();
        services.AddScoped<IAlunoRepository, AlunoRepository>();
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

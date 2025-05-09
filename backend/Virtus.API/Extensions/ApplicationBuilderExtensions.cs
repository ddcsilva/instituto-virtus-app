namespace Virtus.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigurarSwaggerUI(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    public static void ConfigurarMiddlewares(this WebApplication app)
    {
        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapControllers();
    }
}
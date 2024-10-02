using Reminy.Core.Host.Composition.JsonSerialization;

namespace Reminy.Core.Host;

internal static class Startup
{
    internal static void ConfigureServices(IServiceCollection services)
    {
        //services.AddMediatR(typeof(DomainServicesRegistration).Assembly); // todo

        services.AddAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
                options.JsonSerializerOptions.DictionaryKeyPolicy = new SnakeCaseNamingPolicy();
            });
    }

    internal static void ConfigureApp(WebApplication application)
    {
        if (!application.Environment.IsDevelopment())
            application.UseExceptionHandler("/Error");

        application.UseRouting();
        application.UseAuthorization();
        AddSwagger(application);
        application.MapControllers();
    }

    internal static void Run(WebApplication application, string[] args)
    {
        application.Run();
    }

    private static void AddSwagger(WebApplication application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reminy.Core.Host API v1");
            c.RoutePrefix = string.Empty;
        });
    }
}
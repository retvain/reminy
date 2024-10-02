using MediatR;
using Reminy.Core.DomainServices;
using Reminy.Core.Host.Composition.JsonSerialization;

namespace Reminy.Core.Host;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(DomainServicesRegistration).Assembly);

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

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
            app.UseExceptionHandler("/Error");

        app.UseRouting();
        app.UseAuthorization();
        AddSwagger(app);
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

    private static void AddSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reminy.Core.Host API v1");
            c.RoutePrefix = string.Empty;
        });
    }
}
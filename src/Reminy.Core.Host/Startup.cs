using MediatR;
using Reminy.Core.DomainServices;
using Reminy.Core.Host.Composition.JsonSerialization;
using Reminy.Core.Host.Composition.Postgres;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.Host;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMediatR(typeof(DomainServicesRegistration).Assembly)
            .AddDomainServices()
            .AddPostgres()
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
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

    public void Run(WebApplication app, string[] args)
    {
        if (args.Length != 0)
        {
            var arg = args.First();

            switch (arg)
            {
                case "-migrate":
                    var migrator = app.Services.GetRequiredService<IMigrator>();
                    migrator.Migrate();

                    return;

                default:
                    throw new Exception($"Unexpected argument: {arg}");
            }
        }

        app.Run();
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
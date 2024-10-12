using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminy.Core.Host;
using Reminy.Core.IntegrationTests.Data;
using Reminy.Core.TestDataInitialization.Tables;

namespace Reminy.Core.IntegrationTests.Tools;

internal sealed class TestWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override IHostBuilder CreateHostBuilder()
    {
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(b => { b.UseStartup<Startup>(); });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<NotesTable>();
            services.AddSingleton<TagsTable>();
            services.AddSingleton<NoteTagsTable>();
            services.AddSingleton<NoteInitializer>();
        });
    }
}
namespace Reminy.Core.Host;
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();

        Startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        Startup.ConfigureApp(app);

        Startup.Run(app, args);
    }
}
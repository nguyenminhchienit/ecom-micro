using Common.Logging;
using Inventory.API;
using Inventory.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Inventory API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    builder.Services.AddConfigureServices(builder.Configuration);
    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));


    var app = builder.Build();

    app.UseInfrastructure();


    Log.Information("Start Inventory API up");

    app.MigrateDatabase().Run();


}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down Inventory API complete");
    Log.CloseAndFlush();
}

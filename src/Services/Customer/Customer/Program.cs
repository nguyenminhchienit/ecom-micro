using Common.Logging;
using Customer.API.Controller;
using Customer.API.Extensions;
using Customer.API.Extentions;
using Customer.API.Persistence;
using Customer.API.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Customer API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();
    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.MapCustumerApi();

    app.UseInfrastructure();

    app.MigrateDatabase<CustomerContext>()
         .Run();

    
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
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LoggingLib;

/// <summary>
/// Добавление логгирования
/// </summary>
public static class LoggingStartUp
{
    /// <summary>
    /// Поключение Serilog
    /// </summary>
    public static IHostBuilder UseInfraSerilog(this IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        builder.UseSerilog(configureLogger: ((context, provider, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(provider)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }));

        return builder;
    }
}
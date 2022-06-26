using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Providers;
using Providers.Persistence;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

string Namespace = typeof(Startup).Namespace;

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", Namespace);
    var host = BuildWebHost(configuration, args);

    Log.Information("Applying migrations ({ApplicationContext})...", Namespace);


    host.MigrateDbContext<ProviderDbContext>((context, services) =>
    {
        new ProviderDbContextSeed()
            .SeedAsync(context)
            .Wait();
    });

    Log.Information("Starting web host ({ApplicationContext})...", Namespace);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Namespace);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Namespace)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}
IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
WebHost.CreateDefaultBuilder(args)
         .ConfigureKestrel(options =>
         {

             options.Listen(IPAddress.Any, 80, listenOptions =>
             //options.Listen(IPAddress.Any, 5005, listenOptions =>
             {
                 listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                 //listenOptions.UseHttps();

             });

             //options.Listen(IPAddress.Any, 49155, listenOptions =>
             options.Listen(IPAddress.Any, 443, listenOptions =>
             {
                 listenOptions.Protocols = HttpProtocols.Http2;
             });

         })
       .CaptureStartupErrors(false)
       .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
       .UseStartup<Startup>()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .Build();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();


    return builder.Build();
}
namespace Providers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

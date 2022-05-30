using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PickPoint.Test.Api;
using Serilog;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile($"appsettings.{environment}.json", true, false)
            .AddEnvironmentVariables()
            .Build();

        Log.Logger = LoggerHelper.CreateLogger(configuration);

        try
        {
            CreateHostBuilder(args)
                .ConfigureAppConfiguration(c => c.AddConfiguration(configuration))
                .Build()
                .Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The Application failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                    webBuilder.CaptureStartupErrors(false);
                });
}
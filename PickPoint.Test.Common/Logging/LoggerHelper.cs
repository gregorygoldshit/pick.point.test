using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;

public static class LoggerHelper
{
    public static ILogger CreateLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}

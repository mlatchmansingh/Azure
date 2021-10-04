using System;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventHubAppFunction.Application;
using EventHubAppFunction.UI.Services;

[assembly: FunctionsStartup(typeof(EventHubAppFunction.Startup))]
namespace EventHubAppFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            FunctionsHostBuilderContext ctx = builder.GetContext();

            builder.Services.AddLogging(loggingBuilder =>
            {
                using var bsp = builder.Services.BuildServiceProvider();
                loggingBuilder.AddSerilog();
            });

            builder.Services.AddScoped<IEventProcessor, EventProcessor>();
        }
    }
}

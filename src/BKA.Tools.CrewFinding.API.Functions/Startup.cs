using BKA.Tools.CrewFinding.API.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BKA.Tools.CrewFinding.API.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Register your services here. For example:
            // builder.Services.AddSingleton<IMyService, MyService>();
        }
    }
}
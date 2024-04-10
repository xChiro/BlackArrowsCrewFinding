using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        
        services.AddTransient<IKeySecretsProvider, KeySecretsProvider>(_ =>
        {
            var secretClient = new SecretClient(new Uri(config["KeyVault:endpoint"] ?? string.Empty), new DefaultAzureCredential());
            return new KeySecretsProvider(secretClient);
        });
        
        services.AddTransient<IDatabaseSettingsProvider<Container>, DatabaseSettingsProvider>(provider =>
        {
            var keySecretsProvider = provider.GetService<IKeySecretsProvider>()!;
            return new DatabaseSettingsProvider(keySecretsProvider, config);
        });
    }
}
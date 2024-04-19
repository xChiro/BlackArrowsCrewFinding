using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings.KeyVault;
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
        
        services.AddSingleton<IKeySecretsProvider, KeySecretsProvider>(_ =>
        {
            var secretClientOptions = new SecretClientOptions();
            secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
            var secretClient = new SecretClient(new Uri(config["KeyVault:endpoint"] ?? string.Empty), new DefaultAzureCredential(), secretClientOptions);
            
            return new KeySecretsProvider(secretClient);
        });
        
        services.AddTransient<IDatabaseSettingsProvider<Container>, DatabaseSettingsProvider>(provider =>
        {
            var keySecretsProvider = provider.GetService<IKeySecretsProvider>()!;
            return new DatabaseSettingsProvider(keySecretsProvider, config);
        });
    }
}
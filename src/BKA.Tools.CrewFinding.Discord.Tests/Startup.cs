using BKA.Tools.CrewFinding.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.Discord.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var config = BuildConfiguration();
        var keySecretsProvider = KeyVaultInitializer.CreateKeySecretsProvider(config["keyVault:endpoint"]!);

        RegisterDependencies(services, keySecretsProvider, config);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();
    }

    private static void RegisterDependencies(IServiceCollection services, IKeySecretProvider keySecretsProvider,
        IConfigurationRoot config)
    {
        var discordToken = keySecretsProvider.GetSecret(config["keyVault:discordBotToken"]!);

        services.AddSingleton(_ =>
            new DiscordSettings(discordToken, config["discord:guildId"]!, config["discord:parentId"]!));

        services.AddTransient<IDiscordHttpClient>(provider =>
        {
            var discordSettings = provider.GetRequiredService<DiscordSettings>();
            return new DiscordHttpClient(discordSettings);
        });
    }
}
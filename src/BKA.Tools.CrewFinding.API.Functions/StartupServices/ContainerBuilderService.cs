using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.KeyVault;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class ContainerBuilderService
{
    public static ContainerBuilder CreateContainerBuilder()
    {
        var keySecretsProvider = new KeySecretProviderBuilder(Configuration.GetEnvironmentVariable("keyVaultEndpoint")).Build();
        var azureKey = keySecretsProvider.GetSecret(Configuration.GetEnvironmentVariable("keyVaultAzureKeyName"));

        return new ContainerBuilder(Configuration.GetEnvironmentVariable("cosmosDBEndpoint"), azureKey);
    }
}
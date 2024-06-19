using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.KeyVault;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class ContainerBuilderService
{
    public static CosmosDbContainerBuilder CreateContainerBuilder(KeySecretProvider keySecretsProvider)
    {
        var azureKey = keySecretsProvider.GetSecret(Configuration.GetEnvironmentVariable("keyVaultAzureKeyName"));

        return new CosmosDbContainerBuilder(Configuration.GetEnvironmentVariable("cosmosDBEndpoint"), azureKey);
    }
}
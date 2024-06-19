using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public abstract class KeyVaultInitializer
{
    public static IKeySecretProvider CreateKeySecretsProvider(string endpoint)
    {
        var secretClientOptions = new SecretClientOptions();
        secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
        var secretClient = new SecretClient(new Uri(endpoint),
            new DefaultAzureCredential(),
            secretClientOptions);

        return new KeySecretProvider(secretClient);
    }
}
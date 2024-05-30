using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeySecretProviderBuilder(string endPoint)
{
    public KeySecretProvider Build()
    {
        var secretClientOptions = new SecretClientOptions();
        secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
        var secretClient = new SecretClient(new Uri(endPoint),
            new DefaultAzureCredential(),
            secretClientOptions);

        return new KeySecretProvider(secretClient);
    }
}
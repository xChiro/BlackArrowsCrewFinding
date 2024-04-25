using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeySecretProviderBuilder
{
    private readonly string _endPoint;

    public KeySecretProviderBuilder(string endPoint)
    {
        _endPoint = endPoint;
    }
    
    public KeySecretProvider Build()
    {
        var secretClientOptions = new SecretClientOptions();
        secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
        var secretClient = new SecretClient(new Uri(_endPoint),
            new DefaultAzureCredential(),
            secretClientOptions);

        return new KeySecretProvider(secretClient);
    }
}
using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeySecretProvider : IKeySecretProvider
{
    private readonly SecretClient _secretClient;

    public KeySecretProvider(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }
    
    public string GetSecret(string secretName)
    {
        return _secretClient.GetSecret(secretName).Value.Value;
    }
}
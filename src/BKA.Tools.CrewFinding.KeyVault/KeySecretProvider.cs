using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeySecretProvider : IKeySecretProvider
{
    private readonly SecretClient _secretClient;

    public KeySecretProvider(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }
    
    public async Task<string> GetSecret(string secretName)
    {
        return (await _secretClient.GetSecretAsync(secretName)).Value.Value;
    }
}
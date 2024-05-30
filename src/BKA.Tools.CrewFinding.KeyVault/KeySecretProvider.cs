using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeySecretProvider(SecretClient secretClient) : IKeySecretProvider
{
    public string GetSecret(string secretName)
    {
        return secretClient.GetSecret(secretName).Value.Value;
    }
}
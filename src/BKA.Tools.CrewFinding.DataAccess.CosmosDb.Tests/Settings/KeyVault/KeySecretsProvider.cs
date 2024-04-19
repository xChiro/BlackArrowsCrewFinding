using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings.KeyVault;

public class KeySecretsProvider : IKeySecretsProvider
{
    private readonly SecretClient _secretClient;

    public KeySecretsProvider(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }
    
    public async Task<string> GetSecret(string secretName)
    {
        return (await _secretClient.GetSecretAsync(secretName)).Value.Value;
    }
}
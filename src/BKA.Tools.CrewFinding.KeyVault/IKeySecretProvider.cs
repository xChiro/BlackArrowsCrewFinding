namespace BKA.Tools.CrewFinding.KeyVault;

public interface IKeySecretProvider
{
    public Task<string> GetSecret(string secretName);
}
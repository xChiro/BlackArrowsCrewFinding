namespace BKA.Tools.CrewFinding.KeyVault;

public interface IKeySecretProvider
{
    public string GetSecret(string secretName);
}
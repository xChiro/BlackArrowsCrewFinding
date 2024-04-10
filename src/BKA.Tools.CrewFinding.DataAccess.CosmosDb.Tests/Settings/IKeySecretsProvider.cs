using System.Threading.Tasks;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public interface IKeySecretsProvider
{
    public Task<string> GetSecret(string secretName);
}
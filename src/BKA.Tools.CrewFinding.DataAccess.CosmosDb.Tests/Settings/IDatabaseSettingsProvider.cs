namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public interface IDatabaseSettingsProvider<out T> where T : class 
{
    public T GetContainer();
}

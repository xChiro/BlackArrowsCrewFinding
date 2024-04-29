namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public interface IDatabaseSettingsProvider<out T> where T : class 
{
    public T GetCrewContainer();
    public T GetPlayerContainer();
    public T GetDisbandedCrewsContainer();
}

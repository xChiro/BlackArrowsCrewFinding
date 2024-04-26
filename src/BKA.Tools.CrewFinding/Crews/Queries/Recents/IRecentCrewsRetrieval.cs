namespace BKA.Tools.CrewFinding.Crews.Queries.Recents;

public interface IRecentCrewsRetrieval
{
    public Task Retrieve(ICrewsResponse response);
}
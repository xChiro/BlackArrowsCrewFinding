namespace BKA.Tools.CrewFinding.Crews.Queries.Recent;

public interface IRecentCrewsRetrieval
{
    public Task Retrieve(ICrewsResponse response);
}
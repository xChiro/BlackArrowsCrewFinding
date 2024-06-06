namespace BKA.Tools.CrewFinding.Crews.Queries.Retrievs;

public interface IActiveCrewRetrieval
{
    public Task Retrieve(string crewId, ICrewResponse response);
}
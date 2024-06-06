using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Queries.Retrievs;

public class ActiveCrewRetrieval(ICrewQueryRepository crewsQueryRepositoryMock) : IActiveCrewRetrieval
{
    public async Task Retrieve(string crewId, ICrewResponse response)
    {
        var crew = await crewsQueryRepositoryMock.GetCrew(crewId);
        
        if(crew == null)
        {
            throw new CrewNotFoundException(crewId);
        }
        
        response.SetCrew(crew);
    }
}
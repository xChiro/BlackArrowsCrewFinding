using BKA.Tools.CrewFinding.Crews.Ports;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.CrewParties;

public class GetCrewQueryTest
{
    private readonly ICrewQueries _crewQueries;

    public GetCrewQueryTest(ICrewQueries crewQueries, ICrewCommandRepository crewCommandRepository)
    {
        _crewQueries = crewQueries;
    }
    
}
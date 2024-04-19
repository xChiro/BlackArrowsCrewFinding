using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewQueries : ICrewQueries
{
    public Task<Crew?> GetCrewParty(string crewPartyId)
    {
        throw new NotImplementedException();
    }
}
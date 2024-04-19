namespace BKA.Tools.CrewFinding.Crews.JoinRequests;

public interface ICrewJoiner
{
    Task Join(string playerId, string crewPartyId);
}
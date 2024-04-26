namespace BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

public interface ICrewJoiner
{
    Task Join(string playerId, string crewPartyId);
}
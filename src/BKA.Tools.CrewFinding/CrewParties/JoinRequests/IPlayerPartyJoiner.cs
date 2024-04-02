namespace BKA.Tools.CrewFinding.CrewParties.JoinRequests;

public interface IPlayerPartyJoiner
{
    Task Join(string playerId, string crewPartyId);
}
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.CrewParties.JoinRequests;

public class PlayerPartyJoiner : IPlayerPartyJoiner
{
    private readonly ICrewPartyQueries _crewPartyQueries;
    private readonly ICrewPartyCommands _crewPartyCommands;

    public PlayerPartyJoiner(ICrewPartyQueries crewPartyQueries, ICrewPartyCommands crewPartyCommands)
    {
        _crewPartyQueries = crewPartyQueries;
        _crewPartyCommands = crewPartyCommands;
    }

    public async Task Join(string playerId, string crewPartyId)
    {
        var crewParty = await _crewPartyQueries.GetCrewParty(crewPartyId);

        if (crewParty == null)
        {
            throw new CrewPartyNotFoundException(crewPartyId);
        }

        if (crewParty.IsFull())
            throw new CrewPartyFullException(crewPartyId);

        if (await _crewPartyQueries.PlayerAlreadyInAParty(playerId))
        {
            throw new PlayerMultiplePartiesException();
        }

        await _crewPartyCommands.AddPlayerToCrewParty(playerId, crewPartyId);
    }
}
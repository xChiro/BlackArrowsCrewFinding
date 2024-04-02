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
        var crewParty = await GetCrewParty(crewPartyId);
        ValidateCrewParty(crewParty, crewPartyId);

        if (await IsPlayerAlreadyInAParty(playerId))
        {
            throw new PlayerMultiplePartiesException();
        }

        await _crewPartyCommands.AddPlayerToCrewParty(playerId, crewPartyId);
    }

    private async Task<CrewParty> GetCrewParty(string crewPartyId)
    {
        return await _crewPartyQueries.GetCrewParty(crewPartyId) ??
               throw new CrewPartyNotFoundException(crewPartyId);
    }

    private static void ValidateCrewParty(CrewParty crewParty, string crewPartyId)
    {
        if (crewParty.IsFull())
            throw new CrewPartyFullException(crewPartyId);
    }

    private async Task<bool> IsPlayerAlreadyInAParty(string playerId)
    {
        return await _crewPartyQueries.PlayerAlreadyInAParty(playerId);
    }
}
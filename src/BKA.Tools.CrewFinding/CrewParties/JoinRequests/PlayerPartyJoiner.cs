using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Ports;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.CrewParties.JoinRequests;

public class PlayerPartyJoiner : IPlayerPartyJoiner
{
    private readonly ICrewPartyQueries _crewPartyQueries;
    private readonly ICrewPartyCommands _crewPartyCommands;
    private readonly IPlayerQueries _playersQueries;

    public PlayerPartyJoiner(ICrewPartyQueries crewPartyQueries, ICrewPartyCommands crewPartyCommands,
        IPlayerQueries playersQueries)
    {
        _crewPartyQueries = crewPartyQueries;
        _crewPartyCommands = crewPartyCommands;
        _playersQueries = playersQueries;
    }

    public async Task Join(string playerId, string crewPartyId)
    {
        var getCrewPartyTask = GetCrewParty(crewPartyId);
        var isPlayerInPartyTask = IsPlayerAlreadyInAParty(playerId);

        await Task.WhenAll(getCrewPartyTask, isPlayerInPartyTask);

        var crewParty = getCrewPartyTask.Result;
        ValidateCrewParty(crewParty, crewPartyId);

        if (isPlayerInPartyTask.Result)
            throw new PlayerMultiplePartiesException();

        var player = await _playersQueries.GetPlayer(playerId);

        if (player is null)
            throw new PlayerNotFoundException(playerId);
        
        crewParty.AddMember(player);
        _crewPartyCommands.UpdateMembers(crewPartyId, crewParty.Members);
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
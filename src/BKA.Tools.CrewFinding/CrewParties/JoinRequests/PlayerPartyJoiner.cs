using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

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

    public async Task Join(string playerId, string playerName, string crewPartyId)
    {
        var isPlayerInPartyTask = _crewPartyQueries.IsPlayerInAParty(playerId);
        var validCrewPartyTask = GetValidCrewParty(crewPartyId);

        await Task.WhenAll(isPlayerInPartyTask, validCrewPartyTask);

        if (isPlayerInPartyTask.Result)
            throw new PlayerMultiplePartiesException(playerId);
        
        var crewParty = validCrewPartyTask.Result;

        var player = new Player(playerId, playerName);
        crewParty.AddMember(player);
        
        await _crewPartyCommands.UpdateMembers(crewPartyId, crewParty.Members);
    }

    private async Task<CrewParty> GetValidCrewParty(string crewPartyId)
    {
        var crewParty = await GetCrewParty(crewPartyId);
        ValidateCrewPartyIsNotFull(crewParty, crewPartyId);
        return crewParty;
    }

    private async Task<CrewParty> GetCrewParty(string crewPartyId)
    {
        return await _crewPartyQueries.GetCrewParty(crewPartyId) ?? throw new CrewPartyNotFoundException(crewPartyId);
    }

    private static void ValidateCrewPartyIsNotFull(CrewParty crewParty, string crewPartyId)
    {
        if (crewParty.IsFull())
            throw new CrewPartyFullException(crewPartyId);
    }
}
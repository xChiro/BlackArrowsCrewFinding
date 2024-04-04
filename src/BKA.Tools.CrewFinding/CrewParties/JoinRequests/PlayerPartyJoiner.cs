using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Ports;
using BKA.Tools.CrewFinding.Values;
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
        var crewParty = await GetValidCrewParty(crewPartyId);
        var player = await GetValidPlayer(playerId);

        crewParty.AddMember(player);
        await _crewPartyCommands.UpdateMembers(crewPartyId, crewParty.Members);
    }

    private async Task<CrewParty> GetValidCrewParty(string crewPartyId)
    {
        var crewParty = await GetCrewParty(crewPartyId);
        ValidateCrewPartyIsNotFull(crewParty, crewPartyId);
        return crewParty;
    }

    private async Task<Player?> GetValidPlayer(string playerId)
    {
        ValidatePlayerIsNotInAParty(playerId);
        var player = await GetPlayer(playerId);
        ValidatePlayerExists(player, playerId);
        return player;
    }

    private async Task<CrewParty> GetCrewParty(string crewPartyId)
    {
        return await _crewPartyQueries.GetCrewParty(crewPartyId) ?? throw new CrewPartyNotFoundException(crewPartyId);
    }

    private void ValidateCrewPartyIsNotFull(CrewParty crewParty, string crewPartyId)
    {
        if (crewParty.IsFull())
            throw new CrewPartyFullException(crewPartyId);
    }

    private void ValidatePlayerIsNotInAParty(string playerId)
    {
        if (_crewPartyQueries.PlayerAlreadyInAParty(playerId).Result)
            throw new PlayerMultiplePartiesException();
    }

    private async Task<Player?> GetPlayer(string playerId)
    {
        return await _playersQueries.GetPlayer(playerId);
    }

    private static void ValidatePlayerExists(Player? player, string playerId)
    {
        if (player is null)
            throw new PlayerNotFoundException(playerId);
    }
}
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

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

    private async Task<Player> GetValidPlayer(string playerId)
    {
        if (IsPlayerAlreadyInAParty(playerId))
            throw new PlayerMultiplePartiesException();
        
        var player = await GetPlayer(playerId);
        
        if (IsAValidPlayer(player))
            throw new PlayerNotFoundException(playerId);
        
        return player!;
    }

    private bool IsPlayerAlreadyInAParty(string playerId)
    {
        return _crewPartyQueries.PlayerAlreadyInAParty(playerId).Result;
    }

    private static bool IsAValidPlayer(Player? player)
    {
        return player is null;
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

    private async Task<Player?> GetPlayer(string playerId)
    {
        return await _playersQueries.GetPlayer(playerId);
    }
}
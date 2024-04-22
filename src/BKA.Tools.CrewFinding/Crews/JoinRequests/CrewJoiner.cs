using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Crews.JoinRequests;

public class CrewJoiner : ICrewJoiner
{
    private readonly ICrewQueries _crewQueries;
    private readonly ICrewCommandRepository _crewCommandRepository;
    private readonly IPlayerQueries _playersQueries;

    public CrewJoiner(ICrewQueries crewQueries, ICrewCommandRepository crewCommandRepository,
        IPlayerQueries playersQueries)
    {
        _crewQueries = crewQueries;
        _crewCommandRepository = crewCommandRepository;
        _playersQueries = playersQueries;
    }

    public async Task Join(string playerId, string crewPartyId)
    {
        var crewParty = await GetValidCrewParty(crewPartyId);
        var player = await GetValidPlayer(playerId);

        crewParty.AddMember(player);
        await _crewCommandRepository.UpdateMembers(crewPartyId, crewParty.Members);
    }

    private async Task<Crew> GetValidCrewParty(string crewPartyId)
    {
        var crewParty = await GetCrewParty(crewPartyId);
        
        return crewParty;
    }

    private async Task<Player> GetValidPlayer(string playerId)
    {
        if (IsPlayerAlreadyInAParty(playerId))
            throw new PlayerMultipleCrewsException();
        
        var player = await GetPlayer(playerId);
        
        if (IsAValidPlayer(player))
            throw new PlayerNotFoundException(playerId);
        
        return player!;
    }

    private bool IsPlayerAlreadyInAParty(string playerId)
    {
        return _playersQueries.PlayerAlreadyInACrew(playerId).Result;
    }

    private static bool IsAValidPlayer(Player? player)
    {
        return player is null;
    }

    private async Task<Crew> GetCrewParty(string crewPartyId)
    {
        return await _crewQueries.GetCrewParty(crewPartyId) ?? throw new CrewNotFoundException(crewPartyId);
    }

    private async Task<Player?> GetPlayer(string playerId)
    {
        return await _playersQueries.GetPlayer(playerId);
    }
}
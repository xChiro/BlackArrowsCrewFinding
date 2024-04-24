using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Crews.JoinRequests;

public class CrewJoiner : ICrewJoiner
{
    private readonly ICrewQueryRepository _crewQueryRepository;
    private readonly ICrewCommandRepository _crewCommandRepository;
    private readonly IPlayerQueryRepository _playersQueryRepository;

    public CrewJoiner(ICrewQueryRepository crewQueryRepository, ICrewCommandRepository crewCommandRepository,
        IPlayerQueryRepository playersQueryRepository)
    {
        _crewQueryRepository = crewQueryRepository;
        _crewCommandRepository = crewCommandRepository;
        _playersQueryRepository = playersQueryRepository;
    }

    public async Task Join(string playerId, string crewPartyId)
    {
        var crewParty = await GetValidCrewParty(crewPartyId);
        var player = await GetValidPlayer(playerId);

        crewParty.AddMember(player);
        await _crewCommandRepository.UpdateMembers(crewPartyId, crewParty.Players);
    }

    private async Task<Crew> GetValidCrewParty(string crewPartyId)
    {
        var crewParty = await GetCrewParty(crewPartyId);
        
        return crewParty;
    }

    private async Task<Player> GetValidPlayer(string playerId)
    {
        if (await IsPlayerAlreadyInAParty(playerId))
            throw new PlayerMultipleCrewsException();
        
        var player = await GetPlayer(playerId);
        
        if (IsAValidPlayer(player))
            throw new PlayerNotFoundException(playerId);
        
        return player!;
    }

    private async Task<bool> IsPlayerAlreadyInAParty(string playerId)
    {
        return await _crewQueryRepository.IsPlayerInActiveCrew(playerId);
    }

    private static bool IsAValidPlayer(Player? player)
    {
        return player is null;
    }

    private async Task<Crew> GetCrewParty(string crewPartyId)
    {
        return await _crewQueryRepository.GetCrew(crewPartyId) ?? throw new CrewNotFoundException(crewPartyId);
    }

    private async Task<Player?> GetPlayer(string playerId)
    {
        return await _playersQueryRepository.GetPlayer(playerId);
    }
}
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

public class CrewJoiner(
    ICrewValidationRepository crewValidationRepository,
    ICrewQueryRepository crewQueryRepository,
    ICrewCommandRepository crewCommandRepository,
    IPlayerQueryRepository playersQueryRepository,
    IUserSession userSession)
    : ICrewJoiner
{
    public async Task Join(string crewPartyId)
    {
        var crewParty = await GetValidCrewParty(crewPartyId);
        var player = await GetValidPlayer(userSession.GetUserId());

        crewParty.AddMember(player);
        await crewCommandRepository.UpdateMembers(crewPartyId, crewParty.Members);
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
        return await crewValidationRepository.IsPlayerInActiveCrew(playerId);
    }

    private static bool IsAValidPlayer(Player? player)
    {
        return player is null;
    }

    private async Task<Crew> GetCrewParty(string crewPartyId)
    {
        return await crewQueryRepository.GetCrew(crewPartyId) ?? throw new CrewNotFoundException(crewPartyId);
    }

    private async Task<Player?> GetPlayer(string playerId)
    {
        return await playersQueryRepository.GetPlayer(playerId);
    }
}
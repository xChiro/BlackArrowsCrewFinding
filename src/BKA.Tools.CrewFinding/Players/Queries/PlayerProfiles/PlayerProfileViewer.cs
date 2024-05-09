using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public class PlayerProfileViewer(IPlayerQueryRepository playerQueryMock, ICrewQueryRepository crewQueryRepository) : IPlayerProfileViewer
{
    public async Task View(string playerId, IPlayerProfileResponse response)
    {
        var playerTask = playerQueryMock.GetPlayer(playerId);
        var activeCrewTask = crewQueryRepository.GetActiveCrewByPlayerId(playerId);

        await Task.WhenAll(playerTask, activeCrewTask);

        var player = playerTask.Result;
        if (player is null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        var activeCrew = activeCrewTask.Result;

        if (activeCrew is not null)
        {
            response.SetResponse(player, activeCrew.Id, activeCrew.Name.Value);
        }
        else
        {
            response.SetResponse(player);
        }
    }
}
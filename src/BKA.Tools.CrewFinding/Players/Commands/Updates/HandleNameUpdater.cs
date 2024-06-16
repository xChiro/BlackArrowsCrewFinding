using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Commands.Updates;

public class HandleNameUpdater(
    IPlayerQueryRepository playerQueryRepository,
    IPlayerCommandRepository playerCommandRepository,
    IUserSession userSession,
    int maxNameLength,
    int minNameLength)
    : IHandleNameUpdater
{
    public async Task Update(string newName)
    {
        var player = await playerQueryRepository.GetPlayer(userSession.GetUserId());

        if (player is null)
        {
            throw new PlayerNotFoundException(userSession.GetUserId());
        }

        player.UpdateName(newName, minNameLength, maxNameLength);
        await playerCommandRepository.UpdateName(player.Id, player.CitizenName.Value);
    }
}
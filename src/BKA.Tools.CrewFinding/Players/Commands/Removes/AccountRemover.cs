using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Commands.Removes;

public class AccountRemover(
    ICrewQueryRepository crewQueryRepository,
    ICrewCommandRepository crewCommandRepository,
    IPlayerCommandRepository playerCommandRepository,
    IUserSession session) : IAccountRemover
{
    public async Task Remove()
    {
        var playerId = session.GetUserId();
        var historyDeletionTask = crewCommandRepository.DeletePlayerHistory(playerId);
        var activeCrew = await crewQueryRepository.GetActiveCrewByPlayerId(playerId);
        
        var removePlayerFromCrewTask = RemovePlayerFromActiveCrew(activeCrew, playerId);

        Task.WaitAll(playerCommandRepository.Delete(playerId), historyDeletionTask, removePlayerFromCrewTask);
    }

    private Task RemovePlayerFromActiveCrew(Crew? activeCrew, string playerId)
    {
        var removePlayerFromCrewTask = Task.CompletedTask;
        
        if (IsCaptain(activeCrew, playerId))
        {
            removePlayerFromCrewTask = crewCommandRepository.DeleteCrew(activeCrew!.Id);
        }
        else if (activeCrew != null)
        {
            removePlayerFromCrewTask = crewCommandRepository.UpdateMembers(activeCrew.Id,
                activeCrew.Members.Where(member => member.Id != playerId));
        }

        return removePlayerFromCrewTask;
    }

    private static bool IsCaptain(Crew? activeCrew, string playerId)
    {
        return activeCrew != null && activeCrew.Captain.Id == playerId;
    }
}
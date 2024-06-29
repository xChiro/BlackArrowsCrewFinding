using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.Players.Commands;

public class AccountRemover(
    ICrewQueryRepository crewQueryRepository,
    ICrewCommandRepository crewCommandRepository,
    IUserSession session,
    IPlayerCommandRepository playerCommandRepository) : IAccountRemover
{
    public async Task Remove()
    {
        var playerId = session.GetUserId();
        await crewCommandRepository.DeletePlayerHistory(playerId);

        var activeCrew = await crewQueryRepository.GetActiveCrewByPlayerId(playerId);

        if (activeCrew != null && activeCrew.Captain.Id == playerId)
        {
            await crewCommandRepository.DeleteCrew(activeCrew.Id);
        }
        else if (activeCrew != null)
        {
            await crewCommandRepository.UpdateMembers(activeCrew.Id,
                activeCrew.Members.Where(member => member.Id != playerId));
        }

        await playerCommandRepository.Delete(playerId);
    }
}
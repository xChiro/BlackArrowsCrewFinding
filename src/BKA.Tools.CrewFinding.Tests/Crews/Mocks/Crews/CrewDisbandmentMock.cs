using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewDisbandmentMock(string crewId, string? voiceChannelId) : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        output?.SetResult(crewId, voiceChannelId);
        return Task.CompletedTask;
    }
}
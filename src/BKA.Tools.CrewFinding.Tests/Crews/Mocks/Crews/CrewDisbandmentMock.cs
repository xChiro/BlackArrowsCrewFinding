using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewDisbandmentMock(string crewId) : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        output?.SetResult(crewId);
        return Task.CompletedTask;
    }
}
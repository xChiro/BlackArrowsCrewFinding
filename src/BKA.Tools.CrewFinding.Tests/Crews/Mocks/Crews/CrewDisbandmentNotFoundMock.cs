using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewDisbandmentNotFoundMock : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        throw new CrewNotFoundException();
    }
}
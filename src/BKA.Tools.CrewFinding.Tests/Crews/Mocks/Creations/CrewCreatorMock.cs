using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Creations;

public class CrewCreatorMock(string name) : ICrewCreator
{
    public Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        output.SetResponse(Guid.NewGuid().ToString(), name);
        return Task.CompletedTask;
    }
}
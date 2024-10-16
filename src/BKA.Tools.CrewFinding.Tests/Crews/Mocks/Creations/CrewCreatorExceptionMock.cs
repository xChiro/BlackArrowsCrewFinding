using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Creations;

public class CrewCreatorExceptionMock<T> : ICrewCreator where T : Exception, new()
{
    public Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        throw new T();
    }
}
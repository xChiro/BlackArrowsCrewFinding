using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewCreatorExceptionMock<T> : ICrewCreator where T : Exception, new()
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse output)
    {
        throw new T();
    }
}
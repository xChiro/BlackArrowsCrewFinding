namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public interface ICrewCreator
{
    public Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output);
}
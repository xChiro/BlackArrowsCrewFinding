namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public interface ICrewCreator
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse output);
}
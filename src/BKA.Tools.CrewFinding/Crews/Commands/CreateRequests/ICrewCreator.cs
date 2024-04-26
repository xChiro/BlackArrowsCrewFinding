namespace BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;

public interface ICrewCreator
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse crewCreatorResponse);
}
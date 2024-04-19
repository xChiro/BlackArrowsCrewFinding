namespace BKA.Tools.CrewFinding.Crews.CreateRequests;

public interface ICrewCreator
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse crewCreatorResponse);
}
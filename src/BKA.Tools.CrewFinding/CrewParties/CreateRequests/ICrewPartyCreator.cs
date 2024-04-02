namespace BKA.Tools.CrewFinding.CrewParties.CreateRequests;

public interface ICrewPartyCreator
{
    public Task Create(CrewPartyCreatorRequest request, ICrewPartyCreatorResponse crewPartyCreatorResponse);
}
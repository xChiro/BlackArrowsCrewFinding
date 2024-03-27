namespace BKA.Tools.CrewFinding.CrewParties.Creators;

public interface ICrewPartyCreator
{
    public Task Create(CrewPartyCreatorRequest request, ICrewPartyCreatorResponse crewPartyCreatorResponse);
}
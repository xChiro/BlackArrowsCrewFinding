namespace BKA.Tools.CrewFinding.CrewParties;

public interface ICrewPartyCreator
{
    public Task Create(CrewPartyCreatorRequest request);
}
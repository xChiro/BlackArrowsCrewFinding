namespace BKA.Tools.CrewFinding.CrewParties;

public interface ICrewPartyCreator
{
    void Create(string captainName, int totalCrew, Location location, string[] languagesAbbrevs);
}
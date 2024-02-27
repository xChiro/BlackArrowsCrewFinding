namespace BKA.Tools.CrewFinding.CrewParties;

public interface ICrewPartyCreator
{
    public void Create(CrewPartyCreatorRequest request);
}

public record CrewPartyCreatorRequest(
    string CaptainName,
    int TotalCrew,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
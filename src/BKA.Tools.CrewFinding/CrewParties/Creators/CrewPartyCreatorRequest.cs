using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties.Creators;

public record CrewPartyCreatorRequest(
    string captainId,
    int TotalCrew,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
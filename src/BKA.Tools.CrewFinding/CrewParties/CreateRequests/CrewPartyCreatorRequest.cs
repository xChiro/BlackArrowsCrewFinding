using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties.CreateRequests;

public record CrewPartyCreatorRequest(
    string CaptainId,
    int TotalCrew,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
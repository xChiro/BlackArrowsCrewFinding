using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Crews.CreateRequests;

public record CrewCreatorRequest(
    string CaptainId,
    int TotalCrew,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
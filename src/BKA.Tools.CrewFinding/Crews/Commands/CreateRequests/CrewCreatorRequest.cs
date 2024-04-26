using BKA.Tools.CrewFinding.Commons.Values;

namespace BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;

public record CrewCreatorRequest(
    string CaptainId,
    int CrewSize,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
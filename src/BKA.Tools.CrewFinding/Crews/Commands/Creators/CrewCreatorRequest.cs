using BKA.Tools.CrewFinding.Commons.Values;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public record CrewCreatorRequest(
    int CrewSize,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "");
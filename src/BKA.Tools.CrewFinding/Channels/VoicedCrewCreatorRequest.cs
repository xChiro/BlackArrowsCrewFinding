using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Channels;

public record VoicedCrewCreatorRequest(
    int CrewSize,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "",
    string CustomChannelLink = "") : CrewCreatorRequest(CrewSize, Location, LanguagesAbbrevs, ActivityName, Description);
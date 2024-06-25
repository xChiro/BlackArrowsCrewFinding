using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Create;

public class CreateCrewFunctionRequest
{
    public int CrewSize { get; set; }
    public string System { get; set; }
    public string PlanetarySystem { get; set; }
    public string PlanetMoon { get; set; }
    public string Place { get; set; }
    public string[] LanguagesAbbrevs { get; set; }
    public string ActivityName { get; set; }
    public string Description { get; set; }
    public string CustomChannelLink { get; set; }

    public ICrewCreatorRequest ToCrewCreatorRequest()
    {
        return new VoicedCrewCreatorRequest(
            CrewSize,
            new Location(System, PlanetarySystem, PlanetMoon, Place),
            LanguagesAbbrevs,
            ActivityName,
            Description,
            CustomChannelLink);
    }
}
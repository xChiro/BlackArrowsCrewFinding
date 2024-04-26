using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;

namespace BKA.Tools.CrewFinding.API.Functions.CrewCreations;

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

    public CrewCreatorRequest ToCrewCreatorRequest(string userId)
    {
        return new CrewCreatorRequest(
            userId,
            CrewSize,
            new Location(System, PlanetarySystem, PlanetMoon, Place),
            LanguagesAbbrevs,
            ActivityName,
            Description);
    }
}
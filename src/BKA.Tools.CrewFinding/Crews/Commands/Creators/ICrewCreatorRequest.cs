using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public interface ICrewCreatorRequest
{
    int CrewSize { get; }
    Location Location { get; }
    string[] LanguagesAbbrevs { get; }
    string ActivityName { get; }
    string Description { get; }

    Crew ToCrew(Player captain, int playersAllowed);
}
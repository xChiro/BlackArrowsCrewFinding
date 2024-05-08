using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public record CrewCreatorRequest(
    int CrewSize,
    Location Location,
    string[] LanguagesAbbrevs,
    string ActivityName,
    string Description = "")

{
    public Crew ToCrew(Player captain, int playersAllowed)
    {
        var maxPlayersAllowed = CrewSize > playersAllowed ? playersAllowed : CrewSize;
        
        return new Crew(captain, Location,
            LanguageCollections.CreateFromAbbrevs(LanguagesAbbrevs), 
            PlayerCollection.CreateEmpty(maxPlayersAllowed),
            Activity.Create(ActivityName, Description));
    }
};
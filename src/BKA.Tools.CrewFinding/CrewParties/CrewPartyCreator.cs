using BKA.Tools.CrewFinding.CrewParties.Values;
using BKA.Tools.CrewFinding.Cultures;

namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewPartyCreator : ICrewPartyCreator
{
    private readonly int _maxCrewAllowed;
    private readonly ICrewPartyCommands _commands;

    public CrewPartyCreator(ICrewPartyCommands commands, int maxCrewAllowed)
    {
        _maxCrewAllowed = maxCrewAllowed;
        _commands = commands;
    }

    public void Create(CrewPartyCreatorRequest request)
    {
        var crewName = new CrewName(request.CaptainName);
        var captain = new Captain(request.CaptainName);
        var maxCrew = new CrewNumber(request.TotalCrew, _maxCrewAllowed);
        var languageCollections = LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs);
        var activity = Activity.Create(request.ActivityName, request.Description);
        
        var crewParty = new CrewParty(crewName, request.Location, languageCollections, maxCrew, activity, DateTime.UtcNow);

        _commands.SaveCrewParty(captain, crewParty);
    }
}


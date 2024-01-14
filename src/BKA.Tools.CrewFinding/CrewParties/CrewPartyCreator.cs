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

    public void Create(string captainName, int totalCrew, Location location, string[] languagesAbbrevs, string activityName)
    {
        if (captainName is "")
            throw new ArgumentException("Captain name cannot be empty", nameof(captainName));

        var crewName = new CrewName(captainName);
        var captain = new Captain(captainName);
        var maxCrew = new MaxCrewNumber(totalCrew, _maxCrewAllowed);
        var languageCollections = LanguageCollections.CreateFromAbbrevs(languagesAbbrevs);
        var activity = Activity.Create(activityName);
        
        var crewParty = new CrewParty(crewName, location, languageCollections, maxCrew, activity, DateTime.UtcNow);

        _commands.SaveCrewParty(captain, crewParty);
    }
}


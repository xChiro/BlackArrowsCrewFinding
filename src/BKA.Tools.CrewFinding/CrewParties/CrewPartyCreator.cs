using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Cultures.Exceptions;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewPartyCreator : ICrewPartyCreator
{
    private readonly int _maxCrewAllowed;
    private readonly ICrewPartyCommands _commands;
    private readonly ICrewPartyQueries _crewPartyQueries;

    public CrewPartyCreator(ICrewPartyCommands commands, ICrewPartyQueries crewPartyQueries, int maxCrewAllowed)
    {
        _maxCrewAllowed = maxCrewAllowed;
        _commands = commands;
        _crewPartyQueries = crewPartyQueries;
    }

    public async Task Create(CrewPartyCreatorRequest request)
    {
        if(await _crewPartyQueries.CaptainHasCreatedParty(request.CaptainName))
            throw new CaptainMultiplePartiesException();
        
        var crewName = new CrewName(request.CaptainName);
        var captain = new Captain(request.CaptainName);
        var maxCrew = new CrewNumber(request.TotalCrew, _maxCrewAllowed);
        var languageCollections = LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs);
        var activity = Activity.Create(request.ActivityName, request.Description);
        
        var crewParty = new CrewParty(crewName, request.Location, languageCollections, maxCrew, activity, DateTime.UtcNow);

        _commands.SaveCrewParty(captain, crewParty);
    }
}
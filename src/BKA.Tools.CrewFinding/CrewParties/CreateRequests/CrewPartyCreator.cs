using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.CrewParties.CreateRequests;

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

    public async Task Create(CrewPartyCreatorRequest request, ICrewPartyCreatorResponse crewPartyCreatorResponse)
    {
        if(await _crewPartyQueries.IsPlayerInAParty(request.CaptainId))
            throw new PlayerMultiplePartiesException();
        
        var captain = new Player(request.CaptainId, request.CaptainName);
        
        var crewParty = CreateCrewParty(captain, request);
        await _commands.CreateCrewParty(crewParty);
        
        crewPartyCreatorResponse.SetResponse(crewParty.Id);
    }

    private CrewParty CreateCrewParty(Player captain, CrewPartyCreatorRequest request)
    {
        return new CrewParty(captain, new CrewName(captain.Name), request.Location,
            LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs),
            new CrewCapacity(request.TotalCrew, _maxCrewAllowed),
            Activity.Create(request.ActivityName, request.Description), DateTime.UtcNow);
    }
}
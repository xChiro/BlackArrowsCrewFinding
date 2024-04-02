using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.CrewParties.CreateRequests;

public class CrewPartyCreator : ICrewPartyCreator
{
    private readonly int _maxCrewAllowed;
    private readonly IPlayerQueries _playerQueries;
    private readonly ICrewPartyCommands _commands;
    private readonly ICrewPartyQueries _crewPartyQueries;

    public CrewPartyCreator(ICrewPartyCommands commands, ICrewPartyQueries crewPartyQueries, int maxCrewAllowed,
        IPlayerQueries playerQueries)
    {
        _maxCrewAllowed = maxCrewAllowed;
        _playerQueries = playerQueries;
        _commands = commands;
        _crewPartyQueries = crewPartyQueries;
    }

    public async Task Create(CrewPartyCreatorRequest request, ICrewPartyCreatorResponse crewPartyCreatorResponse)
    {
        var playerPartyTask = _crewPartyQueries.PlayerAlreadyInAParty(request.CaptainId);
        var playerTask = _playerQueries.GetPlayer(request.CaptainId);

        await Task.WhenAll(playerPartyTask, playerTask);

        if (playerPartyTask.Result)
            throw new PlayerMultiplePartiesException();

        var captain = playerTask.Result;

        if (captain == null)
            throw new PlayerNotFoundException(request.CaptainId);

        var crewName = new CrewName(captain.Name);
        var maxCrew = new CrewNumber(request.TotalCrew, _maxCrewAllowed);
        var languageCollections = LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs);
        var activity = Activity.Create(request.ActivityName, request.Description);

        var crewParty = new CrewParty(crewName, request.Location, languageCollections, maxCrew, activity,
            DateTime.UtcNow);
        
        var id = await _commands.SaveCrewParty(captain, crewParty);
        crewPartyCreatorResponse.SetResponse(id);
    }
}
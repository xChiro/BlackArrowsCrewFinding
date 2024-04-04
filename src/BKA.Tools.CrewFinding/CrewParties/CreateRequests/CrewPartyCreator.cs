using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Ports;
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
        var captain = await GetAndValidateCaptain(request.CaptainId);
        var crewParty = CreateCrewParty(captain, request);

        var id = await _commands.SaveCrewParty(captain, crewParty);

        crewPartyCreatorResponse.SetResponse(id);
    }

    private CrewParty CreateCrewParty(Player captain, CrewPartyCreatorRequest request)
    {
        return new CrewParty(
            new CrewName(captain.Name),
            request.Location,
            LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs),
            new CrewCapacity(request.TotalCrew, _maxCrewAllowed),
            Activity.Create(request.ActivityName, request.Description),
            DateTime.UtcNow);
    }

    private async Task<Player> GetAndValidateCaptain(string captainId)
    {
        var playerInPartyTask = _crewPartyQueries.PlayerAlreadyInAParty(captainId);
        var captainTask = _playerQueries.GetPlayer(captainId);

        await Task.WhenAll(playerInPartyTask, captainTask);

        ValidatePlayer(captainId, playerInPartyTask.Result, captainTask.Result);

        return captainTask.Result!;
    }

    private static void ValidatePlayer(string captainId, bool playerInPartyResult, Player? captainTaskResult)
    {
        if (captainTaskResult is null)
            throw new PlayerNotFoundException(captainId);
        if (playerInPartyResult)
            throw new PlayerMultiplePartiesException();
    }
}
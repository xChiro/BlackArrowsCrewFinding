using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Crews.CreateRequests;

public class CrewCreator : ICrewCreator
{
    private readonly IPlayerQueryRepository _playerQueryRepository;
    private readonly ICrewCommandRepository _commandRepository;
    private readonly ICrewValidationRepository _crewValidationRepository;
    private readonly int _maxPlayersAllowed;

    public CrewCreator(ICrewCommandRepository commandRepository, ICrewValidationRepository crewValidationRepository,
        IPlayerQueryRepository playerQueryRepository, int maxPlayersAllowed)
    {
        _playerQueryRepository = playerQueryRepository;
        _maxPlayersAllowed = maxPlayersAllowed;
        _commandRepository = commandRepository;
        _crewValidationRepository = crewValidationRepository;
    }

    public async Task Create(CrewCreatorRequest request, ICrewCreatorResponse crewCreatorResponse)
    {
        var captain = await TryToGetValidCaptain(request.CaptainId);
        var crew = InitializeNewCrew(captain, request);

        await _commandRepository.CreateCrew(crew);
        crewCreatorResponse.SetResponse(crew.Id);
    }

    private Crew InitializeNewCrew(Player captain, CrewCreatorRequest request)
    {
        var maxPlayersAllowed = request.CrewSize > _maxPlayersAllowed ? _maxPlayersAllowed : request.CrewSize;
        
        return new Crew(captain, new CrewName(captain.CitizenName), request.Location,
            LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs), 
            PlayerCollection.CreateEmpty(maxPlayersAllowed),
            Activity.Create(request.ActivityName, request.Description));
    }

    private async Task<Player> TryToGetValidCaptain(string captainId)
    {
        var playerInCrewTask = _crewValidationRepository.IsPlayerInActiveCrew(captainId);
        var captainTask = _playerQueryRepository.GetPlayer(captainId);

        await Task.WhenAll(playerInCrewTask, captainTask);

        ValidatePlayer(captainId, playerInCrewTask.Result, captainTask.Result);

        return captainTask.Result!;
    }

    private static void ValidatePlayer(string captainId, bool playerInCrewResult, Player? captainTaskResult)
    {
        if (captainTaskResult is null)
            throw new PlayerNotFoundException(captainId);
        if (playerInCrewResult)
            throw new PlayerMultipleCrewsException();
    }
}
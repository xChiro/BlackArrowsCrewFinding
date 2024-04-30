using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public class CrewCreator(
    ICrewCommandRepository commandRepository,
    ICrewValidationRepository crewValidationRepository,
    IPlayerQueryRepository playerQueryRepository,
    IUserSession userSession,
    int playersAllowed) : ICrewCreator
{
    public async Task Create(CrewCreatorRequest request, ICrewCreatorResponse crewCreatorResponse)
    {
        var captain = await TryToGetValidCaptain(userSession.GetUserId());
        var crew = InitializeNewCrew(captain, request);

        await commandRepository.CreateCrew(crew);
        crewCreatorResponse.SetResponse(crew.Id);
    }

    private Crew InitializeNewCrew(Player captain, CrewCreatorRequest request)
    {
        var maxPlayersAllowed = request.CrewSize > playersAllowed ? playersAllowed : request.CrewSize;
        
        return new Crew(captain, new CrewName(captain.CitizenName), request.Location,
            LanguageCollections.CreateFromAbbrevs(request.LanguagesAbbrevs), 
            PlayerCollection.CreateEmpty(maxPlayersAllowed),
            Activity.Create(request.ActivityName, request.Description));
    }

    private async Task<Player> TryToGetValidCaptain(string captainId)
    {
        var playerInCrewTask = crewValidationRepository.IsPlayerInActiveCrew(captainId);
        var captainTask = playerQueryRepository.GetPlayer(captainId);

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
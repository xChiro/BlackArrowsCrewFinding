using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewActSteps(
    CrewContext crewContext,
    PlayerContext playerContext,
    PlayerRepositoryContext playerRepositoryContext,
    CrewRepositoriesContext crewRepositoriesContext,
    CrewCreationResultsContext crewCreationResultsContext)
{
    [When(@"the player attempts to create a Crew with missing location information")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingLocationInformation()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateDefaultCrewPartyWithoutLocation());
    }

    [When(@"the player creates a Crew with the following details:")]
    public Task WhenThePlayerCreatesACrewWithTheFollowingDetails(Table crewPartyDetails)
    {
        crewContext.FillData(crewPartyDetails);
        return CreateAndStoreCrew(crewContext.ToRequest());
    }

    [When(@"the player attempts to create a Crew with missing MaxCrewSize")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingMaxCrewSize()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrew(crewContext.MaxPlayerAllowed));
    }

    [When(@"the player attempts to create a Crew with missing languages")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingLanguages()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrewPartyWithMissingLanguages());
    }

    [When(@"the player attempts to create a Crew with missing activity information")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingActivityInformation()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrew(1));
    }

    [When(@"the player attempts to create a new Crew")]
    public async Task WhenThePlayerAttemptsToCreateANewCrew()
    {
        try
        {
            var crewCreatorRequest = CrewCreatorRequestFactory.CreateCrew(1);
            await CreateAndStoreCrew(crewCreatorRequest);
        }
        catch (Exception ex)
        {
            crewCreationResultsContext.Exception = ex;
        }
    }

    private async Task CreateAndStoreCrew(CrewCreatorRequest crewCreatorRequest)
    {
        var crewPartyCreator = new CrewCreator(crewRepositoriesContext.CommandRepositoryMock,
            crewRepositoriesContext.ValidationRepositoryMocks,
            playerRepositoryContext.PlayerQueryRepositoryMock,
            new UserSessionMock(playerContext.PlayerId), 10);

        await crewPartyCreator.Create(crewCreatorRequest,
            crewCreationResultsContext.CrewCreatorResponseMock);
    }
}
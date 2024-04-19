using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyRepositoryArrangeSteps
{
    private readonly CrewPartyRepositoriesContext _crewPartyRepositoriesContext;

    public CrewPartyRepositoryArrangeSteps(CrewPartyRepositoriesContext crewPartyRepositoriesContext)
    {
        _crewPartyRepositoriesContext = crewPartyRepositoriesContext;
    }

    [Given(@"an existing Crew Party from other player")]
    public void GivenAnExistingCrewPartyFromOtherPlayer()
    {
        const string playerId = "playerId";
        const string citizenName = "playerName";

        var crewParties = new CrewParty[]
        {
            new(Player.Create(playerId, citizenName),
                new CrewName(playerId),
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                new CrewCapacity(1, 4), Activity.Default())
        };
        
        _crewPartyRepositoriesContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();
        _crewPartyRepositoriesContext.CrewPartyQueriesMocks = new CrewPartyQueriesMock(crewParties);
    }

    [Given(@"there is not a Crew Party")]
    public void GivenThereIsNotACrewParty()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"the player is already a member of a Crew Party")]
    public void GivenThePlayerIsAlreadyAMemberOfACrewParty()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"an existing Crew Party at maximum capacity from other player")]
    public void GivenAnExistingCrewPartyAtMaximumCapacityFromOtherPlayer()
    {
        const string playerId = "playerId";
        const string citizenName = "playerName";

        var crewParties = new CrewParty[]
        {
            new(Player.Create(playerId, citizenName),
                new CrewName(playerId),
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                new CrewCapacity(1, 1), Activity.Default())
        };
        
        _crewPartyRepositoriesContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();
        _crewPartyRepositoriesContext.CrewPartyQueriesMocks = new CrewPartyQueriesMock(crewParties);
    }
}
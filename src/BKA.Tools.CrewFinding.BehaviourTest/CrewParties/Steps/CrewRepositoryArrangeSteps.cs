using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewRepositoryArrangeSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;

    public CrewRepositoryArrangeSteps(CrewRepositoriesContext crewRepositoriesContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
    }

    [Given(@"an existing Crew from other player")]
    public void GivenAnExistingCrewFromOtherPlayer()
    {
        const string playerId = "playerId";
        const string citizenName = "playerName";

        var crewParties = new Crew[]
        {
            new(Player.Create(playerId, citizenName),
                new CrewName(playerId),
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                PlayerCollection.CreateEmpty(4),
                Activity.Default())
        };

        _crewRepositoriesContext.CrewQueryRepositoryMocks = new CrewQueryRepositoryMock(crewParties);
    }

    [Given(@"there is not a Crew")]
    public void GivenThereIsNotACrew()
    {
        _crewRepositoriesContext.CrewQueryRepositoryMocks = new CrewNotFoundQueryRepositoryMock();
    }

    [Given(@"an existing Crew at maximum capacity from other player")]
    public void GivenAnExistingCrewAtMaximumCapacityFromOtherPlayer()
    {
        const string playerId = "playerId";
        const string citizenName = "playerName";
        var members = PlayerCollection.CreateWithSingle(Player.Create("3412343", "Rowan"), 1);

        var crews = new Crew[]
        {
            new(Player.Create(playerId, citizenName),
                new CrewName(playerId),
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                members,
                Activity.Default())
        };

        _crewRepositoriesContext.CrewCommandRepositoryMock = new CrewCommandRepositoryMock();
        _crewRepositoriesContext.CrewQueryRepositoryMocks = new CrewQueryRepositoryMock(crews);
    }
}
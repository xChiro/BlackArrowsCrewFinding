using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

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
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                PlayerCollection.CreateEmpty(4),
                Activity.Default())
        };

        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crewParties);
    }

    [Given(@"there is not a Crew")]
    public void GivenThereIsNotACrew()
    {
        _crewRepositoriesContext.ValidationRepositoryMocks = new CrewNotFoundValidationRepositoryMock();
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
                Location.DefaultLocation(),
                LanguageCollections.Default(),
                members,
                Activity.Default())
        };

        _crewRepositoriesContext.CommandRepositoryMock = new CrewCommandRepositoryMock();
        _crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock();
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crews);
    }

    [Given(@"there is the following crews in the system")]
    public void GivenThereIsTheFollowingCrewsInTheSystem(Table table)
    {
        var crews = table.Rows.Select(row =>
        {
            var playerId = Guid.NewGuid().ToString();
            var playerName = row["CaptainHandle"];
            var maxCrewSize = int.Parse(row["MaxCrewSize"]);
            var language = LanguageCollections.Default();
            
            var members = PlayerCollection.CreateWithSingle(Player.Create("3412343", "Rowan"), maxCrewSize);

            return new Crew(Player.Create(playerId, playerName),
                new Location(row["System"], row["PlanetarySystem"], row["PlanetMoon"], row["Location"]),
                language,
                members,
                Activity.Create(row["Activity"], row["Description"]));
        }).ToArray();

        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crews);
    }
}
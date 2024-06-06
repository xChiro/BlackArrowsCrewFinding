using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewRepositoryArrangeSteps(
    CrewRepositoriesContext crewRepositoriesContext,
    SystemSettingContext systemSettingContext)
{
    private const string PlayerId = "playerId";
    private const string CitizenName = "playerName";

    [Given(@"an existing Crew from other player")]
    public void GivenAnExistingCrewFromOtherPlayer()
    {
        var crewParties = new Crew[]
        {
            new(Player.Create(PlayerId, CitizenName),
                Location.Default(),
                LanguageCollections.Default(),
                PlayerCollection.CreateEmpty(4),
                Activity.Default())
        };

        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crewParties);
    }

    [Given(@"there is not a Crew")]
    public void GivenThereIsNotACrew()
    {
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewNotFoundValidationRepositoryMock();
    }

    [Given(@"an existing Crew at maximum capacity from other player")]
    public void GivenAnExistingCrewAtMaximumCapacityFromOtherPlayer()
    {
        var members = PlayerCollection.CreateWithSingle(Player.Create("3123", "Adam"), 1);

        var crews = new Crew[]
        {
            new(Player.Create(PlayerId, CitizenName),
                Location.Default(),
                LanguageCollections.Default(),
                members,
                Activity.Default())
        };

        crewRepositoriesContext.CommandRepositoryMock = new CrewCommandRepositoryMock();
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock();
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crews);
    }

    [Given(@"there is the following crews in the system")]
    public void GivenThereIsTheFollowingCrewsInTheSystem(Table table)
    {
        var crews = table.Rows.Select(CreateCrewFromRow).ToArray();
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(crews);
    }

    private Crew CreateCrewFromRow(TableRow row)
    {
        var playerId = Guid.NewGuid().ToString();
        var playerName = row["CaptainHandle"];
        var maxCrewSize = int.Parse(row["MaxCrewSize"]);
        var members = CreateMembers(maxCrewSize, Convert.ToInt32(row["CurrentCrewSize"]));
        var location = new Location(row["System"], row["PlanetarySystem"], row["PlanetMoon"], row["Location"]);
        var activity = Activity.Create(row["Activity"], row["Description"]);
        var createdAt = GetCreatedAt(row);
        var crewId = GetCrewId(row);
        var player = Player.Create(playerId, playerName);

        return new Crew(crewId, player, location, LanguageCollections.Default(), members, activity, createdAt);
    }

    private DateTime GetCreatedAt(TableRow row)
    {
        var createdAt = DateTime.UtcNow;
        
        if (row.TryGetValue("IsExpired", out var isExpiredString) && bool.Parse(isExpiredString))
        {
            createdAt = createdAt.AddHours(-systemSettingContext.LeastCrewTimeThreshold);
        }
        
        return createdAt;
    }

    private static string GetCrewId(TableRow row)
    {
        return row.TryGetValue("CrewId", out var crewId) ? crewId : Guid.NewGuid().ToString();
    }

    private static PlayerCollection CreateMembers(int maxCrewSize, int totalMembers)
    {
        var members = PlayerCollection.CreateEmpty(maxCrewSize);

        for (var i = 0; i < totalMembers; i++)
        {
            var memberId = Guid.NewGuid().ToString();
            members.Add(Player.Create(memberId, "Member"));
        }

        return members;
    }
}
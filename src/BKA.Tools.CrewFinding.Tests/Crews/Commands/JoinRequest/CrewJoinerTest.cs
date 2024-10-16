using System;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Players;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.JoinRequest;

public class CrewJoinerTest
{
    private readonly PlayerQueryRepositoryAlwaysValidMock _playerQueryRepositoryAlwaysValidMock = new("Rowan");
    private const string PlayerId = "1";

    [Fact]
    public async Task Attempt_To_Join_NonExisting_Party()
    {
        // Arrange
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(null, crewPartyCommandsMock, _playerQueryRepositoryAlwaysValidMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewNotFoundException>(() => playerPartyJoiner.Join("1234"));

        // Assert
        MembersShouldBeEmpty(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_Full_Party()
    {
        // Arrange
        var members = PlayerCollection.CreateWithSingle(Player.Create("1231412", "Allan",2, 16), 1);
        var crew = InitializeCrew(members);
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crew, crewPartyCommandsMock, _playerQueryRepositoryAlwaysValidMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewFullException>(() => playerPartyJoiner.Join(crew.Id));

        // Assert
        MembersShouldBeEmpty(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Already_In_A_Party_Then_The_Party_Have_Two_Members()
    {
        // Arrange
        var members = PlayerCollection.CreateWithSingle(Player.Create(PlayerId, "Rowan",2, 16), 4);

        var crew = InitializeCrew(members);
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();

        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(PlayerId, "Rowan");
        var playerPartyJoiner = CreatePlayerPartyJoiner(crew, crewPartyCommandsMock, playerQueriesValidationMock, true);

        // Act & Assert 
        await ExecuteAndAssertException<PlayerMultipleCrewsException>(
            () => playerPartyJoiner.Join(crew.Id));

        // Assert
        MembersShouldBeEmpty(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_A_Party_By_Non_Existing_Player()
    {
        // Arrange
        var crew = InitializeCrew(PlayerCollection.CreateEmpty(4));
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();

        var userSessionMock = new UserSessionMock(Guid.NewGuid().ToString());
        var playerPartyJoiner = CreatePlayerPartyJoiner(crew, crewPartyCommandsMock,
            new PlayerQueryRepositoryValidationMock("1", "Rowan"), userSession: userSessionMock);

        // Act & Assert 
        await ExecuteAndAssertException<PlayerNotFoundException>(
            () => playerPartyJoiner.Join(crew.Id));

        // Assert
        MembersShouldBeEmpty(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Join_To_Party_Successfully()
    {
        // Arrange
        var crew = InitializeCrew(PlayerCollection.CreateEmpty(4));
        var crewPartyCommands = new CrewCommandRepositoryMock();
        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(PlayerId, "Rowan");

        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crew, crewPartyCommands, playerQueriesValidationMock);

        // Act
        await playerPartyJoiner.Join(crew.Id);

        // Assert
        crewPartyCommands.Crew.Members.Should().Satisfy(member => member.Id == PlayerId);
        crew.Members.Count().Should().Be(1);
    }

    [Fact]
    public async Task Join_To_Party_With_Existing_Member_Successfully()
    {
        // Arrange
        var existingMember = Player.Create("ExistingMemberId", "ExistingMemberName",2, 18);
        const string newMemberId = "NewMemberId";
        const string newMemberName = "NewMemberName";

        var members = PlayerCollection.CreateWithSingle(existingMember, 4);
        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(newMemberId, newMemberName);

        var crew = InitializeCrew(members);
        var crewPartyCommands = new CrewCommandRepositoryMock();
        var playerPartyJoiner = CreatePlayerPartyJoiner(crew, crewPartyCommands, playerQueriesValidationMock, 
            userSession: new UserSessionMock(newMemberId));

        // Act
        await playerPartyJoiner.Join(crew.Id);

        // Assert
        crewPartyCommands.Crew.Members.Should().Contain(member => member.Id == newMemberId);
        crewPartyCommands.Crew.Members.Should().Contain(member => member.CitizenName.Value == newMemberName);
        crew.Members.Count().Should().Be(2);
    }

    private static void MembersShouldBeEmpty(CrewCommandRepositoryMock crewCommandRepositoryMock)
    {
        crewCommandRepositoryMock.Crew.Members.Should().BeNullOrEmpty();
    }

    private static Crew InitializeCrew(PlayerCollection playerCollection)
    {
        var crewParty = new Crew(Player.Create("1", "Rowan",2, 16),
            new Location("Stanton", "Crusader", "Crusader", "Seraphim Station"), LanguageCollections.Default(),
            playerCollection, Activity.Default());

        return crewParty;
    }

    private static CrewJoiner CreatePlayerPartyJoiner(Crew? crew, ICrewCommandRepository? crewPartyCommands,
        IPlayerQueryRepository playerQueryRepository, bool playerInParty = false, IUserSession? userSession = null)
    {
        crewPartyCommands ??= new CrewCommandRepositoryMock();
        ICrewQueryRepository crewQueriesRepositoryMock =
            crew == null ? new CrewQueryRepositoryEmptyMock() : new CrewQueryRepositoryMock(crews: [crew]);
        var crewValidationRepositoryMock = new CrewValidationRepositoryMock(playerInParty);

        return new CrewJoiner(crewValidationRepositoryMock, crewQueriesRepositoryMock, crewPartyCommands,
            playerQueryRepository, userSession ?? new UserSessionMock(PlayerId));
    }

    private static async Task ExecuteAndAssertException<T>(Func<Task> act)
        where T : Exception
    {
        // Act
        await Assert.ThrowsAsync<T>(act);
    }
}
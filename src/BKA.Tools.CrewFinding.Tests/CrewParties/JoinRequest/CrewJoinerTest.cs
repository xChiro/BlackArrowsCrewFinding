using System;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.JoinRequest;

public class CrewJoinerTest
{
    private readonly PlayerQueryRepositoryAlwaysValidMock _playerQueryRepositoryAlwaysValidMock = new("Rowan");
    private const string PlayerId = "1";
    private const string CrewPartyId = "2";

    [Fact]
    public async Task Attempt_To_Join_NonExisting_Party()
    {
        // Arrange
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerPartyJoiner = CreatePlayerPartyJoiner(new CrewQueryRepositoryMock(), crewPartyCommandsMock,
            _playerQueryRepositoryAlwaysValidMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewNotFoundException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId));

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_Full_Party()
    {
        // Arrange
        var members = Members.CreateSingle(Player.Create("1231412", "Allan"), 1);
        var crewPartyQueries = new CrewQueryRepositoryMock(CrewPartyId, CrewPartyCreator(members));
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock,
            _playerQueryRepositoryAlwaysValidMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewFullException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId));

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Already_In_A_Party_Then_The_Party_Have_Two_Members()
    {
        // Arrange
        var crew = Members.CreateSingle(Player.Create(PlayerId, "Rowan"), 4);

        var crewPartyQueries = new CrewQueryRepositoryMock(CrewPartyId, CrewPartyCreator(crew), true);
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();

        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(PlayerId, "Rowan");
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock, playerQueriesValidationMock);

        // Act & Assert 
        await ExecuteAndAssertException<PlayerMultipleCrewsException>(
            () => playerPartyJoiner.Join(PlayerId, CrewPartyId));

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Does_Not_Exist()
    {
        // Arrange
        var crewPartyQueries = new CrewQueryRepositoryMock(CrewPartyId, CrewPartyCreator(Members.CreateEmpty(4)));
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();

        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock,
            new PlayerQueryRepositoryValidationMock("1", "Rowan"));

        // Act & Assert 
        await ExecuteAndAssertException<PlayerNotFoundException>(
            () => playerPartyJoiner.Join("412412", CrewPartyId));

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Join_To_Party_Successfully()
    {
        // Arrange
        var crewPartyCreator = CrewPartyCreator(Members.CreateEmpty(4));
        var crewPartyQueries = new CrewQueryRepositoryMock(CrewPartyId, crewPartyCreator);
        var crewPartyCommands = new CrewCommandRepositoryMock(CrewPartyId);
        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(PlayerId, "Rowan");
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommands, playerQueriesValidationMock);

        // Act
        await playerPartyJoiner.Join(PlayerId, CrewPartyId);

        // Assert
        crewPartyCommands.Members.Should().Satisfy(member => member.Id == PlayerId);
        crewPartyCreator.Members.Count().Should().Be(1);
    }

    [Fact]
    public async Task Join_To_Party_With_Existing_Member_Successfully()
    {
        // Arrange
        var existingMember = Player.Create("ExistingMemberId", "ExistingMemberName");
        const string newMemberId = "NewMemberId";
        const string newMemberName = "NewMemberName";

        var members = Members.CreateSingle(existingMember, 4);
        var crewPartyCreator = CrewPartyCreator(members);
        var crewPartyQueries = new CrewQueryRepositoryMock(CrewPartyId, crewPartyCreator);
        var crewPartyCommands = new CrewCommandRepositoryMock(CrewPartyId);
        var playerQueriesValidationMock = new PlayerQueryRepositoryValidationMock(newMemberId, newMemberName);
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommands, playerQueriesValidationMock);

        // Act
        await playerPartyJoiner.Join(newMemberId, CrewPartyId);

        // Assert
        crewPartyCreator.Members.Count().Should().Be(2);
    }

    private static void MembersShouldBeNull(CrewCommandRepositoryMock crewCommandRepositoryMock)
    {
        crewCommandRepositoryMock.Members.Should().BeNull();
    }

    private static Crew CrewPartyCreator(Members members)
    {
        var crewParty = new Crew(Player.Create("1", "Rowan"), new CrewName("Crew Party of Rowan"),
            new Location("Stanton", "Crusader", "Crusader", "Seraphim Station"), LanguageCollections.Default(),
            members, Activity.Default());

        return crewParty;
    }

    private static CrewJoiner CreatePlayerPartyJoiner(ICrewQueryRepository crewQueryRepository,
        ICrewCommandRepository? crewPartyCommands, IPlayerQueryRepository playerQueryRepository)
    {
        crewPartyCommands ??= new CrewCommandRepositoryMock();

        return new CrewJoiner(crewQueryRepository, crewPartyCommands, playerQueryRepository);
    }

    private static async Task ExecuteAndAssertException<T>(Func<Task> act)
        where T : Exception
    {
        // Act
        await Assert.ThrowsAsync<T>(act);
    }
}
using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.JoinRequests;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.JoinRequest;

public class PlayerPartyJoinerTest
{
    private const string PlayerId = "1";
    private const string CrewPartyId = "2";

    [Fact]
    public async Task Attempt_To_Join_NonExisting_Party()
    {
        // Arrange
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var playerPartyJoiner = CreatePlayerPartyJoiner(new CrewPartyQueriesMock(), crewPartyCommandsMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewPartyNotFoundException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId),
            $"Crew party not found with {CrewPartyId}");

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_Full_Party()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(false, CrewPartyId, CrewPartyCreator(10, 10));
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock);

        // Act & Assert
        await ExecuteAndAssertException<CrewPartyFullException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId),
            $"Crew party is full with {CrewPartyId}");

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Already_In_A_Party()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(true, CrewPartyId, CrewPartyCreator(1, 10));
        var crewPartyCommandsMock = new CrewPartyCommandsMock();

        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock);

        // Act & Assert 
        await ExecuteAndAssertException<PlayerMultiplePartiesException>(
            () => playerPartyJoiner.Join(PlayerId, CrewPartyId), null);

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Does_Not_Exist()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(false, CrewPartyId, CrewPartyCreator(1, 10));
        var crewPartyCommandsMock = new CrewPartyCommandsMock();

        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommandsMock,
            new PlayerQueriesValidationMock("1", "Rowan"));

        // Act & Assert 
        await ExecuteAndAssertException<PlayerNotFoundException>(
            () => playerPartyJoiner.Join("412412", CrewPartyId), null);

        // Assert
        MembersShouldBeNull(crewPartyCommandsMock);
    }

    [Fact]
    public async Task Join_To_Party_Successfully()
    {
        // Arrange
        var crewPartyCreator = CrewPartyCreator(1, 10);
        var crewPartyQueries = new CrewPartyQueriesMock(false, CrewPartyId, crewPartyCreator);
        var crewPartyCommands = new CrewPartyCommandsMock(CrewPartyId);
        var playerQueriesValidationMock = new PlayerQueriesValidationMock(PlayerId, "Rowan");
        var playerPartyJoiner =
            CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommands, playerQueriesValidationMock);

        // Act
        await playerPartyJoiner.Join(PlayerId, CrewPartyId);

        // Assert
        crewPartyCommands.Members.Should().Satisfy(member => member.Id == PlayerId);
        crewPartyCreator.CrewCapacity.Current.Should().Be(2);
    }

    private static void MembersShouldBeNull(CrewPartyCommandsMock crewPartyCommandsMock)
    {
        crewPartyCommandsMock.Members.Should().BeNull();
    }

    private static CrewParty CrewPartyCreator(int currentCrewSize, int maxCrewSize)
    {
        var crewParty = new CrewParty(Player.Create("1", "Rowan"), new CrewName("Crew Party of Rowan"),
            new Location("Stanton", "Crusader", "Crusader", "Seraphim Station"), LanguageCollections.Default(),
            new CrewCapacity(currentCrewSize, maxCrewSize), Activity.Default());

        return crewParty;
    }

    private static PlayerPartyJoiner CreatePlayerPartyJoiner(ICrewPartyQueries crewPartyQueries,
        ICrewPartyCommands? crewPartyCommands = null, IPlayerQueries? playerQueries = null)
    {
        crewPartyCommands ??= new CrewPartyCommandsMock();

        return new PlayerPartyJoiner(crewPartyQueries, crewPartyCommands,
            playerQueries ?? new PlayerQueriesAlwaysValidMock("Rowan"));
    }

    private static async Task ExecuteAndAssertException<T>(Func<Task> act, string? expectedMessage)
        where T : Exception
    {
        // Act
        var exception = await Assert.ThrowsAsync<T>(act);

        // Assert
        if (expectedMessage != null)
        {
            exception.Message.Should().BeEquivalentTo(expectedMessage);
        }
    }
}
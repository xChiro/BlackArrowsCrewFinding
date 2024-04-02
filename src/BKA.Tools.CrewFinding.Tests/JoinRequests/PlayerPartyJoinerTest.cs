using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.CrewParties.JoinRequests;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.JoinRequests;

public class PlayerPartyJoinerTest
{
    private const string PlayerId = "1";
    private const string CrewPartyId = "2";

    [Fact]
    public async Task Attempt_To_Join_NonExisting_Party()
    {
        // Arrange
        var playerPartyJoiner = CreatePlayerPartyJoiner(new CrewPartyQueriesMock());

        // Act & Assert
        await ExecuteAndAssertException<CrewPartyNotFoundException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId),
            $"Crew party not found with {CrewPartyId}");
    }

    [Fact]
    public async Task Attempt_To_Join_Full_Party()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(false, CrewPartyId, CrewPartyCreator(10, 10));
        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries);

        // Act & Assert
        await ExecuteAndAssertException<CrewPartyFullException>(() => playerPartyJoiner.Join(PlayerId, CrewPartyId),
            $"Crew party is full with {CrewPartyId}");
    }

    [Fact]
    public async Task Attempt_To_Join_To_Party_With_Player_Already_In_A_Party()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(true, CrewPartyId, CrewPartyCreator(1, 10));
        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries);

        // Act & Assert 
        await ExecuteAndAssertException<PlayerMultiplePartiesException>(
            () => playerPartyJoiner.Join(PlayerId, CrewPartyId), null);
    }

    [Fact]
    public async Task Join_To_Party_Successfully()
    {
        // Arrange
        var crewPartyQueries = new CrewPartyQueriesMock(false, CrewPartyId, CrewPartyCreator(1, 10));
        var crewPartyCommands = new CrewPartyCommandsMock(CrewPartyId);
        var playerPartyJoiner = CreatePlayerPartyJoiner(crewPartyQueries, crewPartyCommands);

        // Act
        await playerPartyJoiner.Join(PlayerId, CrewPartyId);

        // Assert
        crewPartyCommands.AddedPlayerId.Should().Be(PlayerId);
    }

    private static CrewParty CrewPartyCreator(int currentCrewSize, int maxCrewSize)
    {
        var crewParty = new CrewParty(new CrewName("Crew Party of Rowan"),
            new Location("Stanton", "Crusader", "Crusader", "Seraphim Station"), LanguageCollections.Default(),
            new CrewNumber(currentCrewSize, maxCrewSize), Activity.Default(), DateTime.UtcNow);
        
        return crewParty;
    }

    private static PlayerPartyJoiner CreatePlayerPartyJoiner(ICrewPartyQueries crewPartyQueries,
        ICrewPartyCommands? crewPartyCommands = null)
    {
        crewPartyCommands ??= new CrewPartyCommandsMock();
        
        return new PlayerPartyJoiner(crewPartyQueries, crewPartyCommands);
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

public class PlayerPartyJoiner : IPlayerPartyJoiner
{
    private readonly ICrewPartyQueries _crewPartyQueries;
    private readonly ICrewPartyCommands _crewPartyCommands;

    public PlayerPartyJoiner(ICrewPartyQueries crewPartyQueries, ICrewPartyCommands crewPartyCommands)
    {
        _crewPartyQueries = crewPartyQueries;
        _crewPartyCommands = crewPartyCommands;
    }

    public async Task Join(string playerId, string crewPartyId)
    {
        var crewParty = await _crewPartyQueries.GetCrewParty(crewPartyId);

        if (crewParty == null)
        {
            throw new CrewPartyNotFoundException(crewPartyId);
        }

        if (crewParty.IsFull())
            throw new CrewPartyFullException(crewPartyId);

        if (await _crewPartyQueries.PlayerAlreadyInAParty(playerId))
        {
            throw new PlayerMultiplePartiesException();
        }

        await _crewPartyCommands.AddPlayerToCrewParty(playerId, crewPartyId);
    }
}
using System;

namespace BKA.Tools.CrewFinding.Tests.JoinRequests;

public class PlayerPartyJoinerTest
{
    /*
     *  - Try to join to a party that does not exist
     *  - Try to join to a party that is full
     *  - Try to join to a party with a player that is already in the party
     *  - Join to a party successfully
     */
    
    [Fact]
    public void Attempt_To_Join_NonExisting_Party()
    {
        // Arrange
        const string player = "John Doe";
        
        var playerPartyJoiner = new PlayerPartyJoiner();
        
        // Act & Assert
        //await Assert.ThrowsAsync<CrewPartyNotFoundException>(() => playerPartyJoiner.Join(player, "Captain"));
    }
}

public class CrewPartyNotFoundException : Exception
{
    public CrewPartyNotFoundException(string id) : base($"Crew party not found with {id}")
    {
    }
}

public class PlayerPartyJoiner : IPlayerPartyJoiner
{
    public void Join(string player, string id)
    {
        throw new System.NotImplementedException();
    }
}

public interface IPlayerPartyJoiner
{
    void Join(string player, string partyCaptain);
}
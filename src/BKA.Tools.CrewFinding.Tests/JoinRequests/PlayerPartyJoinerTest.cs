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
    public void Try_to_join_to_a_party_that_does_not_exist()
    {
        // Arrange
        var playerPartyJoiner = new PlayerPartyJoiner();
        var player = "John Doe";
        
        // Act
    }
}

public class PlayerPartyJoiner : IPlayerPartyJoiner
{
    public void Join(string player, string partyCaptain)
    {
        throw new System.NotImplementedException();
    }
}

public interface IPlayerPartyJoiner
{
    void Join(string player, string partyCaptain);
}
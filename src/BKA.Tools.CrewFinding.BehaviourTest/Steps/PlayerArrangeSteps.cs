using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Steps;

[Binding]
public class PlayerArrangeSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext)
{
    [Given(@"a player named (.*)")]
    [Given(@"a player named ""(.*)""")]
    [Given(@"I am a player named ""(.*)""")]
    public void When_givenAPlayerNamed(string userName)
    {
        playerContext.PlayerId = Guid.NewGuid().ToString();
        playerContext.PlayerName = userName;
        
        playerRepositoryContext.PlayerQueryRepositoryMock.Players.Add(
            Player.Create(playerContext.PlayerId, playerContext.PlayerName));
    }

    [Given(@"the following players exist:")]
    public void GivenTheFollowingPlayersExist(Table table)
    {
        foreach (var row in table.Rows)
        {
            var playerId = row["Id"];
            var playerName = row["Name"];
            
            playerRepositoryContext.PlayerQueryRepositoryMock.Players.Add(
                Player.Create(playerId, playerName));
        }
    }

    [Given(@"I am a player logged in with id ""(.*)""")]
    public void GivenIAmAPlayerLoggedInWithId(string playerId)
    {
        playerContext.PlayerId = playerId;
    }
}
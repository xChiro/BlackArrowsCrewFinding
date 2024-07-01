using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class MembersArrangeSteps(
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext,
    CrewContext crewContext)
{
    [Given(@"the player is a member of a Crew")]
    public void GivenThePlayerIsAMemberOfACrew()
    {
        var player = Player.Create(playerContext.PlayerId, playerContext.PlayerName,2, 16);
        var crew = CreateCrew();
        
        crew.AddMember(player);
        SetCrew(crew);
    }

    [Given(@"the player is not a member of any Crew")]
    public void GivenThePlayerIsNotAMemberOfAnyCrew()
    {
        var crew = CreateCrew();
        SetCrew(crew);
    }

    private static Crew CreateCrew()
    {
        const string captainName = "Adam";
        
        return new Crew(
            Player.Create("4312412", captainName,2, 16),
            Location.Default(),
            LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(4),
            Activity.Default()
        );
    }

    private void SetCrew(Crew crew)
    {
        crewContext.CrewId = crew.Id;
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }
}
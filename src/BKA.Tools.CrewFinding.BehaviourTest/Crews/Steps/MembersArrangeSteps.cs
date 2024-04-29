using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class MembersArrangeSteps
{
    private readonly PlayerContext _playerContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly CrewContext _crewContext;

    public MembersArrangeSteps(PlayerContext playerContext, CrewRepositoriesContext crewRepositoriesContext,
        CrewContext crewContext)
    {
        _playerContext = playerContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _crewContext = crewContext;
    }

    [Given(@"the player is a member of a Crew")]
    public void GivenThePlayerIsAMemberOfACrew()
    {
        var player = Player.Create(_playerContext.PlayerId, _playerContext.PlayerName);
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
            Player.Create("4312412", captainName),
            new CrewName(captainName),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(4),
            Activity.Default()
        );
    }

    private void SetCrew(Crew crew)
    {
        _crewContext.CrewId = crew.Id;
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }
}
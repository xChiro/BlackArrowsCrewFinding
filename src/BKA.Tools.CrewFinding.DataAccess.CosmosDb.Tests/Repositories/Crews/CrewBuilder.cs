using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public static class CrewBuilder
{
    public static Crew CreateDefaultCrew()
    {
        var captain = Player.Create("24", "Rowan");
        
        return new Crew(
            captain,
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            Members.CreateSingle(Player.Create("1", "wrerwerwe"), 1),
            Activity.Default());
    }
}
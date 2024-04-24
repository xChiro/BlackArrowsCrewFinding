using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public static class CrewBuilder
{
    public static Crew CreateDefaultCrew(int maxAllowed = 1)
    {
        var captain = Player.Create("24", "Rowan");
        
        return new Crew(
            captain,
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            PlayerCollection.CreateWithSingle(Player.Create("1", "wrerwerwe"), maxAllowed),
            Activity.Default());
    }
}
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public static class CrewBuilder
{
    public static Crew CreateDefaultCrew(int maxAllowed = 1, string captainId = "24")
    {
        var captain = Player.Create(captainId, "Rowan");
        
        return new Crew(
            captain,
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(maxAllowed),
            Activity.Default());
    }
}
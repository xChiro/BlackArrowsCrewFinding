using System;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public static class CrewBuilder
{
    public static Crew CreateDefaultCrew(int maxAllowed = 1, string captainId = "24")
    {
        return CreateDefaultCrew(DateTime.UtcNow, maxAllowed, captainId);
    }
    
    public static Crew CreateDefaultCrew(DateTime createdAt, int maxAllowed = 1, string captainId = "24")
    {
        var captain = Player.Create(captainId, "Rowan", 2, 16);
        
        return new Crew(
            Guid.NewGuid().ToString(),
            captain,
            Location.Default(),
            LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(maxAllowed),
            Activity.Default(),
            createdAt);
    }
}
using System;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Tests.Commons;

public static class CrewBuilder
{
    public static Crew Build(string id, Player captain)
    {
        return new Crew(id, captain, Location.Default(), LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(4), Activity.Default(), DateTime.UtcNow);
    }
}

using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts.Values;

public record CrewLocation(string System, string PlanetarySystem, string PlanetOrMoon, string Place)
{
    public CrewLocation() : this(string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public Location ToLocation()
    {
        return new Location(System, PlanetarySystem, PlanetOrMoon, Place);
    }
}
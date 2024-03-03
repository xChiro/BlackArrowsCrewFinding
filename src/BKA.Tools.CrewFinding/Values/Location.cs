namespace BKA.Tools.CrewFinding.Values;

public record Location(string System, string PlanetarySystem, string PlanetMoon, string Place)
{
    public static Location DefaultLocation() => new("Stanton", "Crusader", "Crusader", "Seraphim Station");

    public virtual bool Equals(Location? other) => other is not null && System == other.System &&
                                                   PlanetarySystem == other.PlanetarySystem &&
                                                   PlanetMoon == other.PlanetMoon && Place == other.Place;

    public override int GetHashCode()
    {
        return HashCode.Combine(System, PlanetarySystem, PlanetMoon, Place);
    }
}
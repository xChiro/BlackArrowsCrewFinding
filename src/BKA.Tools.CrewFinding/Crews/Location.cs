namespace BKA.Tools.CrewFinding.Crews;

public record Location
{
    private const string DefaultSystem = "Stanton";
    private const string DefaultPlanetarySystem = "Crusader";
    private const string DefaultPlanetMoon = "Crusader";
    private const string DefaultPlace = "Seraphim Station";

    public Location(string system, string planetarySystem, string planetMoon, string place)
    {
        System = system == string.Empty ? DefaultSystem : system;
        PlanetarySystem = planetarySystem == string.Empty ? DefaultPlanetarySystem : planetarySystem;
        PlanetMoon = planetMoon == string.Empty ? DefaultPlanetMoon : planetMoon;
        Place = place == string.Empty ? DefaultPlace : place;
    }

    public string System { get; }
    public string PlanetarySystem { get; }
    public string PlanetMoon { get; }
    public string Place { get; }
    
    public static Location Default() =>
        new(DefaultSystem, DefaultPlanetarySystem, DefaultPlanetMoon, DefaultPlace);

    public virtual bool Equals(Location? other) => other is not null && System == other.System &&
                                                   PlanetarySystem == other.PlanetarySystem &&
                                                   PlanetMoon == other.PlanetMoon && Place == other.Place;

    public override int GetHashCode()
    {
        return HashCode.Combine(System, PlanetarySystem, PlanetMoon, Place);
    }
}
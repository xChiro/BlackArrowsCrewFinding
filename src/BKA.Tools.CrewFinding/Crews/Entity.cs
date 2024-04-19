namespace BKA.Tools.CrewFinding.Crews;

public class Entity
{
    public string Id { get; init; }

    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
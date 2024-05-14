namespace BKA.Tools.CrewFinding.Commons;

public class Entity
{
    public string Id { get; init; }

    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
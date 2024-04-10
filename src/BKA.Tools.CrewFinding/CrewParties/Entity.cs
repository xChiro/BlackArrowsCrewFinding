namespace BKA.Tools.CrewFinding.CrewParties;

public class Entity
{
    public string Id { get; init; }

    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
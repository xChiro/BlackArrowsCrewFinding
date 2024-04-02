using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Values;

public record Player
{
    public const int MaxLength = 30;

    public Player(string id, string name)
    {
        if (name is "")
            throw new NameEmptyException();

        if (name.Length > MaxLength)
            throw new NameLengthException(MaxLength);

        Name = name;
        Id = id;
    }

    public string Id { get; set; }

    public string Name { get; }
}
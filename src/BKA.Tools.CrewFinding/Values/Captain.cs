using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Values;

public record Captain
{
    public const int MaxLength = 30;

    public Captain(string Name)
    {
        if (Name is "")
            throw new CaptainNameEmptyException();

        if (Name.Length > MaxLength)
            throw new CaptainNameLengthException(MaxLength);

        this.Name = Name;
    }

    public string Name { get; }
}
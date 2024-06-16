using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.Players;

public record HandlerName
{
    public string Value { get; private set; }

    private HandlerName(string value)
    {
        Value = value;
    }
    
    public static HandlerName Create(string value, int minLength, int maxLength)
    {
        if (value.Length < minLength || value.Length > maxLength)
            throw new HandlerNameLengthException(minLength, maxLength);
        
        return new HandlerName(value);
    }
}
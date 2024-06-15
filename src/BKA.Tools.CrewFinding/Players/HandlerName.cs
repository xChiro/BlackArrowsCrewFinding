using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.Players;

public record HandlerName
{
    public string Value { get; private set; }
    
    public int MinLength { get;  }
    
    public int MaxLength { get;  }

    private HandlerName(string value, int minLength, int maxLength)
    {
        Value = value;
        MinLength = minLength;
        MaxLength = maxLength;
    }
    
    public static HandlerName Create(string value, int minLength, int maxLength)
    {
        if (value.Length < minLength || value.Length > maxLength)
            throw new HandlerNameLengthException(minLength);
        
        return new HandlerName(value, minLength, maxLength);
    }
}
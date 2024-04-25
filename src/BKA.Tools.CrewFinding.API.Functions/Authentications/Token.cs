namespace BKA.Tools.CrewFinding.API.Functions.Authentications;

public class Token
{
    public string Value { get; }

    public Token(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = string.Empty;
        }

        var parts = value.Split(' ');
        if (parts.Length != 2)
        {
            Value = string.Empty;
        }

        Value = parts[1];
    }
}
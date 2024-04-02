namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class NameLengthException : Exception
{
    public NameLengthException(int maxLength) : base($"The captain name must be between 1 and {maxLength} characters.")
    {
    }
}
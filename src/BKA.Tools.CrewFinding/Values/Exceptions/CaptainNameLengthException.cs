namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class CaptainNameLengthException : Exception
{
    public CaptainNameLengthException(int maxLength) : base($"The captain name must be between 1 and {maxLength} characters.")
    {
    }
}
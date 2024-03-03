namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class CaptainNameEmptyException : Exception
{
    public CaptainNameEmptyException() : base("Captain name cannot be empty.")
    {
    }
}
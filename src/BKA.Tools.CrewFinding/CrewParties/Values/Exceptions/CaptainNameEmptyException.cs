namespace BKA.Tools.CrewFinding.CrewParties.Values.Exceptions;

public class CaptainNameEmptyException : Exception
{
    public CaptainNameEmptyException() : base("Captain name cannot be empty.")
    {
    }
}
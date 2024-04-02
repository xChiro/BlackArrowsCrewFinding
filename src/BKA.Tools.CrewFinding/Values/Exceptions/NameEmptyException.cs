namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class NameEmptyException : Exception
{
    public NameEmptyException() : base("Captain name cannot be empty.")
    {
    }
}
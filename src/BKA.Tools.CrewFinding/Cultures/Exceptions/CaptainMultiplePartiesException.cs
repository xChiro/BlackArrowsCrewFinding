namespace BKA.Tools.CrewFinding.Cultures.Exceptions;

public class CaptainMultiplePartiesException : Exception
{
    public CaptainMultiplePartiesException(string message = "A captain can only create one party at a time.") :
        base(message)
    { }
}
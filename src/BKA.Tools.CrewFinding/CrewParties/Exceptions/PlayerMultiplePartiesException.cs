namespace BKA.Tools.CrewFinding.CrewParties.Exceptions;

public class PlayerMultiplePartiesException : Exception
{
    public PlayerMultiplePartiesException(string message = "A player can only create one party at a time.") :
        base(message)
    { }
}
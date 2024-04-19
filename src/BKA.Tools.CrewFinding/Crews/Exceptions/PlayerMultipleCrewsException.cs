namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class PlayerMultipleCrewsException : Exception
{
    public PlayerMultipleCrewsException(string message = "A player can only create one crew at a time.") :
        base(message)
    { }
}
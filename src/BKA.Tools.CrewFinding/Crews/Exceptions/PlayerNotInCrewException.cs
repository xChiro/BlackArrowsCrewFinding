namespace BKA.Tools.CrewFinding.Crews.Exceptions;

public class PlayerNotInCrewException : Exception
{
    public PlayerNotInCrewException() : base("Player is not in the crew")
    {
    }
}
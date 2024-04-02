namespace BKA.Tools.CrewFinding.Values.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(string id) : base($"Player with id {id} was not found")
    { }
}